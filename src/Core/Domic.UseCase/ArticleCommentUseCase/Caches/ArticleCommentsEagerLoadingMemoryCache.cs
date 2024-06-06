using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentUseCase.DTOs.ViewModels;

namespace Domic.UseCase.ArticleCommentUseCase.Caches;

public class ArticleCommentsEagerLoadingMemoryCache : 
    IInternalDistributedCacheHandler<IEnumerable<ArticleCommentsViewModel>>
{
    private readonly IArticleCommentQueryRepository _articleCommentQueryRepository;

    public ArticleCommentsEagerLoadingMemoryCache(IArticleCommentQueryRepository articleCommentQueryRepository) 
        => _articleCommentQueryRepository = articleCommentQueryRepository;

    [Config(Key = Cache.AggregateArticleComments, Ttl = 4 * 24 * 60)]
    public async Task<IEnumerable<ArticleCommentsViewModel>> SetAsync(CancellationToken cancellationToken)
    {
        var comments = await _articleCommentQueryRepository.FindAllEagerLoadingByProjectionAsync(comment =>
            new ArticleCommentsViewModel {
                Id            = comment.Id                                           , 
                OwnerFullName = comment.User.FirstName + " " + comment.User.LastName ,
                ArticleTitle  = comment.Article.Title                                ,
                Comment       = comment.Comment                                      ,
                IsActive      = comment.IsActive == IsActive.Active                  ,
                CreatedAt     = comment.CreatedAt_PersianDate 
            },
            cancellationToken
        );

        return comments;
    }
}