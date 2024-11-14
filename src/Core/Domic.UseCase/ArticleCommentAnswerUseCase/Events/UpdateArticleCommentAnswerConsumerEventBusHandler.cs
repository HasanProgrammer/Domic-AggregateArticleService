using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Events;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Events;

public class UpdateArticleCommentAnswerConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentAnswerUpdated>
{
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public UpdateArticleCommentAnswerConsumerEventBusHandler(
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    ) => _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleCommentAnswers}|{Cache.AggregateArticles}")]
    public void Handle(ArticleCommentAnswerUpdated @event)
    {
        var targetAnswer = _articleCommentAnswerQueryRepository.FindById(@event.Id);

        if (targetAnswer is not null)
        {
            targetAnswer.Answer                = @event.Answer;
            targetAnswer.UpdatedBy             = @event.UpdatedBy;
            targetAnswer.UpdatedRole           = @event.UpdatedRole;
            targetAnswer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetAnswer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentAnswerQueryRepository.Change(targetAnswer);
        }
    }

    public void AfterTransactionHandle(ArticleCommentAnswerUpdated @event){}
}