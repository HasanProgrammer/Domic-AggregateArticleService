using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Events;

namespace Domic.UseCase.ArticleCommentUseCase.Events;

public class UpdateArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository) : IConsumerEventBusHandler<ArticleCommentUpdated>
{
    public Task BeforeHandleAsync(ArticleCommentUpdated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    public async Task HandleAsync(ArticleCommentUpdated @event, CancellationToken cancellationToken)
    {
        var targetComment = await articleCommentQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        if (targetComment is not null)
        {
            targetComment.Comment               = @event.Comment;
            targetComment.UpdatedBy             = @event.UpdatedBy;
            targetComment.UpdatedRole           = @event.UpdatedRole;
            targetComment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetComment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            await articleCommentQueryRepository.ChangeAsync(targetComment, cancellationToken);
        }
    }

    public Task AfterHandleAsync(ArticleCommentUpdated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}