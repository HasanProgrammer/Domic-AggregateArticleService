using Karami.Core.Grpc.AggregateArticle;
using Karami.UseCase.ArticleUseCase.Queries.ReadAllPaginated;

namespace Karami.WebAPI.Frameworks.Extensions.Mappers.ArticleMappers;

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
                PageNumber   = request.PageNumber.Value,
                CountPerPage = request.CountPerPage.Value
            };
        }

        return (T)Request;
    }
}