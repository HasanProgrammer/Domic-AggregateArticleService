using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentUseCase.DTOs;

namespace Domic.UseCase.ArticleCommentUseCase.Caches;

public class ArticleCommentInternalDistributedCache(
    IArticleCommentQueryRepository articleCommentQueryRepository
) : IInternalDistributedCacheHandler<List<ArticleCommentDto>>
{
    [Config(Key = Cache.AggregateArticleComments, Ttl = 4 * 24 * 60)]
    public Task<List<ArticleCommentDto>> SetAsync(CancellationToken cancellationToken) 
        => articleCommentQueryRepository.FindAllByProjectionAsync<ArticleCommentDto>(comment =>
            new ArticleCommentDto {
                Id                = comment.Id                                           , 
                CreatedByFullName = comment.User.FirstName + " " + comment.User.LastName ,
                ArticleTitle      = comment.Article.Title                                ,
                Comment           = comment.Comment                                      ,
                IsActive          = comment.IsActive == IsActive.Active                  ,
                CreatedAt         = comment.CreatedAt_PersianDate 
            },
            cancellationToken
        );
}