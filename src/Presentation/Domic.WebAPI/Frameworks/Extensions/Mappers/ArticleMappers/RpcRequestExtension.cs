using Domic.Core.AggregateArticle.Grpc;
using Domic.UseCase.ArticleUseCase.Queries.ReadAllPaginated;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleMappers;

//Query
public static class RpcRequestExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToQuery<T>(this ReadAllPaginatedRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(ReadAllPaginatedQuery))
        {
            Request = new ReadAllPaginatedQuery {
                UserId       = request.UserId?.Value,
                IsActive     = request.IsActive,
                SearchText   = request.SearchText?.Value,
                PageNumber   = request.PageNumber?.Value,
                CountPerPage = request.CountPerPage?.Value
            };
        }

        return (T)Request;
    }
}