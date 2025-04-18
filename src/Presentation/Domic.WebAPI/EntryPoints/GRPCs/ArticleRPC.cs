using Grpc.Core;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.AggregateArticle.Grpc;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.ArticleUseCase.DTOs;
using Domic.UseCase.ArticleUseCase.Queries.ReadAllPaginated;
using Domic.UseCase.ArticleUseCase.Queries.ReadOne;
using Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleMappers;

namespace Domic.WebAPI.EntryPoints.GRPCs;

public class ArticleRPC(
    IMediator mediator,
    IConfiguration configuration
) : AggregateArticleService.AggregateArticleServiceBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ReadOneResponse> ReadOne(ReadOneRequest request, ServerCallContext context)
    {
        var query = request.ToQuery<ReadOneQuery>();

        var result = await mediator.DispatchAsync<ArticleDto>(query, context.CancellationToken);

        return result.ToRpcResponse<ReadOneResponse>(configuration);
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
            await mediator.DispatchAsync<PaginatedCollection<ArticleDto>>(query, context.CancellationToken);

        return result.ToRpcResponse<ReadAllPaginatedResponse>(configuration);
    }
}