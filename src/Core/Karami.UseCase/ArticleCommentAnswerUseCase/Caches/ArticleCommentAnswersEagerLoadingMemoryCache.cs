using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Caches;

public class ArticleCommentAnswersEagerLoadingMemoryCache : IMemoryCacheSetter<IEnumerable<ArticleCommentAnswersViewModel>>
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