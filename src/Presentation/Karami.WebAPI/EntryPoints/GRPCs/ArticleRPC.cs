using Grpc.Core;
using Karami.Core.Common.ClassHelpers;
using Karami.Core.Grpc.AggregateArticle;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.UseCase.ArticleUseCase.DTOs.ViewModels;
using Karami.UseCase.ArticleUseCase.Queries.ReadAllPaginated;
using Karami.WebAPI.Frameworks.Extensions.Mappers.ArticleMappers;

namespace Karami.WebAPI.EntryPoints.GRPCs;

public class ArticleRPC : AggregateArticleService.AggregateArticleServiceBase
{
    private readonly IMediator      _mediator;
    private readonly IConfiguration _configuration;

    public ArticleRPC(IMediator mediator, IConfiguration configuration)
    {
        _mediator      = mediator;
        _configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ReadAllPaginatedResponse> ReadAllPaginated(ReadAllPaginatedRequest request, 
        ServerCallContext context
    )
    {
        var query = request.ToQuery<ReadAllPaginatedQuery>();

        var result =
            await _mediator.DispatchAsync<PaginatedCollection<ArticlesViewModel>>(query, context.CancellationToken);

        return result.ToRpcResponse<ReadAllPaginatedResponse>(_configuration);
    }
}