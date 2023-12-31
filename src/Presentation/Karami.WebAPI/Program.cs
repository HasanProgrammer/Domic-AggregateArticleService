using Karami.Core.Common.ClassConsts;
using Karami.Core.Infrastructure.Extensions;
using Karami.Core.WebAPI.Extensions;
using Karami.Infrastructure.Extensions.Q;
using Karami.Persistence.Contexts.Q;
using Karami.WebAPI.EntryPoints.GRPCs;
using Karami.WebAPI.Frameworks.Extensions;

/*-------------------------------------------------------------------*/

WebApplicationBuilder builder = WebApplication.CreateBuilder();

#region Configs

builder.WebHost.ConfigureAppConfiguration((context, builder) => builder.AddJsonFiles(context.HostingEnvironment));

#endregion

/*-------------------------------------------------------------------*/

#region Service Container

builder.RegisterHelpers();
builder.RegisterQuerySqlServer<SQLContext>();
builder.RegisterQueryRepositories();
builder.RegisterMessageBroker();
builder.RegisterJobs();
builder.RegisterCaching();
builder.RegisterCommandQueryUseCases();
builder.RegisterGrpcServer();
builder.RegisterEventsSubscriber();
builder.RegisterServices();

builder.Services.AddMvc();

#endregion

/*-------------------------------------------------------------------*/

WebApplication application = builder.Build();

/*-------------------------------------------------------------------*/

//Primary processing

application.Services.AutoMigration<SQLContext>(context => context.Seed());

/*-------------------------------------------------------------------*/

#region Middleware

if (application.Environment.IsProduction())
{
    application.UseHsts();
    application.UseHttpsRedirection();
}

application.UseRouting();

application.UseEndpoints(endpoints => {
    
    endpoints.HealthCheck(application.Services);

    #region GRPC's Services

    endpoints.MapGrpcService<ArticleRPC>();

    #endregion

});

#endregion

/*-------------------------------------------------------------------*/

//HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

application.Run();

/*-------------------------------------------------------------------*/

//For Integration Test

public partial class Program {}