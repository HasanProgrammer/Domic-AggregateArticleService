using System.Data;
using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Events;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Events;

public class InActiveArticleCommentAnswerConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentAnswerInActived>
{
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public InActiveArticleCommentAnswerConsumerEventBusHandler(
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    ) => _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;

    [WithCleanCache(Keies = $"{Cache.AggregateArticleCommentAnswers}|{Cache.AggregateArticles}")]
    [WithTransaction(IsolationLevel = IsolationLevel.ReadUncommitted)]
    public void Handle(ArticleCommentAnswerInActived @event)
    {
        var targetAnswer = _articleCommentAnswerQueryRepository.FindById(@event.Id);

        if (targetAnswer is not null)
        {
            targetAnswer.IsActive              = IsActive.InActive;
            targetAnswer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetAnswer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentAnswerQueryRepository.Change(targetAnswer);
        }
    }
}