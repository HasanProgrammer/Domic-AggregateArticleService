using Domic.Core.Common.ClassHelpers;
using Domic.Core.AggregateArticle.Grpc;
using Domic.Core.Infrastructure.Extensions;
using Domic.UseCase.ArticleUseCase.DTOs.ViewModels;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleMappers;

public static class RpcResponseExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="models"></param>
    /// <param name="configuration"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToRpcResponse<T>(this PaginatedCollection<ArticlesViewModel> models, IConfiguration configuration)
    {
        object Response = null;

        if (typeof(T) == typeof(ReadAllPaginatedResponse))
        {
            Response = new ReadAllPaginatedResponse {
                Code    = configuration.GetValue<int>("StatusCode:SuccessFetchData")    ,
                Message = configuration.GetValue<string>("Message:FA:SuccessFetchData") ,
                Body    = new ReadAllPaginatedResponseBody {
                    Articles = models.Serialize()
                }
            };
        }

        return (T) Response;
    }
}