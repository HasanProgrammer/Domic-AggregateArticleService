using Grpc.Core;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.AggregateArticle.Grpc;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.ArticleUseCase.DTOs.ViewModels;
using Domic.UseCase.ArticleUseCase.Queries.ReadAllPaginated;
using Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleMappers;

namespace Domic.WebAPI.EntryPoints.GRPCs;

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