using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Caches;

public class ArticleCommentAnswersEagerLoadingMemoryCache : 
    IInternalDistributedCacheHandler<IEnumerable<ArticleCommentAnswersViewModel>>
{
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public ArticleCommentAnswersEagerLoadingMemoryCache(
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    ) => _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;

    [Config(Key = Cache.AggregateArticleCommentAnswers, Ttl = 4 * 24 * 60)]
    public async Task<IEnumerable<ArticleCommentAnswersViewModel>> SetAsync(CancellationToken cancellationToken)
    {
        var answers = await _articleCommentAnswerQueryRepository.FindAllEagerLoadingByProjectionAsync(answer =>
            new ArticleCommentAnswersViewModel {
                Id            = answer.Id                                          ,
                OwnerFullName = answer.User.FirstName + " " + answer.User.LastName ,
                ArticleTitle  = answer.Comment.Article.Title                       ,
                Answer        = answer.Answer                                      ,
                IsActive      = answer.IsActive == IsActive.Active                 ,
                CreatedAt     = answer.CreatedAt_PersianDate 
            },
            cancellationToken
        );

        return answers;
    }
}