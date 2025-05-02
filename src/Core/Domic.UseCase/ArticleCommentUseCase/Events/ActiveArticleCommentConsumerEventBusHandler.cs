using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Events;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Events;

public class ActiveArticleCommentConsumerEventBusHandler(
    IArticleCommentQueryRepository       articleCommentQueryRepository,
    IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
) : IConsumerEventBusHandler<ArticleCommentActived>
{
    public Task BeforeHandleAsync(ArticleCommentActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    public async Task HandleAsync(ArticleCommentActived @event, CancellationToken cancellationToken)
    {
        var targetComment = await articleCommentQueryRepository.FindByIdEagerLoadingAsync(@event.Id, cancellationToken);

        if (targetComment is not null)
        {
            targetComment.IsActive              = IsActive.Active;
            targetComment.UpdatedBy             = @event.UpdatedBy;
            targetComment.UpdatedRole           = @event.UpdatedRole;
            targetComment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetComment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            foreach (var answer in targetComment.Answers)
            {
                answer.IsActive              = IsActive.Active;
                answer.UpdatedBy             = @event.UpdatedBy;
                answer.UpdatedRole           = @event.UpdatedRole;
                answer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                answer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            }
        }

        await articleCommentQueryRepository.ChangeAsync(targetComment, cancellationToken);
        await articleCommentAnswerQueryRepository.ChangeRangeAsync(targetComment.Answers, cancellationToken);
    }

    public Task AfterHandleAsync(ArticleCommentActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}