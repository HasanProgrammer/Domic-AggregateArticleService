using System.Data;
using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Events;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Events;

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
            targetAnswer.UpdatedBy             = @event.UpdatedBy;
            targetAnswer.UpdatedRole           = @event.UpdatedRole;
            targetAnswer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetAnswer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentAnswerQueryRepository.Change(targetAnswer);
        }
    }
}