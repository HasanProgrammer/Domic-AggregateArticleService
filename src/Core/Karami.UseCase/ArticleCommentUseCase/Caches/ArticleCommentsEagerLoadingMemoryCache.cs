using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.UseCase.ArticleCommentUseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentUseCase.Caches;

public class ArticleCommentsEagerLoadingMemoryCache : IMemoryCacheSetter<IEnumerable<ArticleCommentsViewModel>>
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