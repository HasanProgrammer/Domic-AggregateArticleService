﻿using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Events;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Events;

public class InActiveArticleCommentAnswerConsumerEventBusHandler(
    IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
) : IConsumerEventBusHandler<ArticleCommentAnswerInActived>
{
    public Task BeforeHandleAsync(ArticleCommentAnswerInActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleCommentAnswers}|{Cache.AggregateArticles}")]
    public async Task HandleAsync(ArticleCommentAnswerInActived @event, CancellationToken cancellationToken)
    {
        var targetAnswer = await articleCommentAnswerQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        if (targetAnswer is not null)
        {
            targetAnswer.IsActive              = IsActive.InActive;
            targetAnswer.UpdatedBy             = @event.UpdatedBy;
            targetAnswer.UpdatedRole           = @event.UpdatedRole;
            targetAnswer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetAnswer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            await articleCommentAnswerQueryRepository.ChangeAsync(targetAnswer, cancellationToken);
        }
    }   

    public Task AfterHandleAsync(ArticleCommentAnswerInActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}