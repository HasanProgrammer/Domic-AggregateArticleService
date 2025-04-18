using Domic.Core.Common.ClassHelpers;
using Domic.Core.AggregateArticle.Grpc;
using Domic.Core.Common.ClassExtensions;
using Domic.Core.Infrastructure.Extensions;
using Domic.UseCase.ArticleUseCase.DTOs;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleMappers;

public static class RpcResponseExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="configuration"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToRpcResponse<T>(this ArticleDto model, IConfiguration configuration)
    {
        object Response = null;

        if (typeof(T) == typeof(ReadOneResponse))
        {
            Response = new ReadOneResponse {
                Code    = configuration.GetSuccessStatusCode()       ,
                Message = configuration.GetSuccessFetchDataMessage() ,
                Body    = new ReadOneResponseBody {
                    Article = model.Serialize()
                }
            };
        }

        return (T) Response;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="models"></param>
    /// <param name="configuration"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToRpcResponse<T>(this PaginatedCollection<ArticleDto> models, IConfiguration configuration)
    {
        object Response = null;

        if (typeof(T) == typeof(ReadAllPaginatedResponse))
        {
            Response = new ReadAllPaginatedResponse {
                Code    = configuration.GetSuccessStatusCode()       ,
                Message = configuration.GetSuccessFetchDataMessage() ,
                Body    = new ReadAllPaginatedResponseBody {
                    Articles = models.Serialize()
                }
            };
        }

        return (T) Response;
    }
}