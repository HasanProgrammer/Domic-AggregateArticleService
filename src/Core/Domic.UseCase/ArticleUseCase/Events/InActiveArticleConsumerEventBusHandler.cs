using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.Article.Events;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.CategoryUseCase.Events;

public class InActiveArticleConsumerEventBusHandler(
    IArticleQueryRepository              articleQueryRepository,
    IArticleCommentQueryRepository       articleCommentQueryRepository,
    IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
) : IConsumerEventBusHandler<ArticleInActived>
{
    public Task BeforeHandleAsync(ArticleInActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public async Task HandleAsync(ArticleInActived @event, CancellationToken cancellationToken)
    {
        var targetArticle = await articleQueryRepository.FindByIdEagerLoadingAsync(@event.Id, cancellationToken);

        targetArticle.IsActive              = IsActive.InActive;
        targetArticle.UpdatedBy             = @event.UpdatedBy;
        targetArticle.UpdatedRole           = @event.UpdatedRole;
        targetArticle.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
        targetArticle.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
        
        foreach (var comment in targetArticle.Comments)
        {
            comment.IsActive              = IsActive.InActive;
            comment.UpdatedBy             = @event.UpdatedBy;
            comment.UpdatedRole           = @event.UpdatedRole;
            comment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            comment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            foreach (var answer in comment.Answers)
            {
                answer.IsActive              = IsActive.InActive;
                answer.UpdatedBy             = @event.UpdatedBy;
                answer.UpdatedRole           = @event.UpdatedRole;
                answer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                answer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            }
        }

        await articleQueryRepository.ChangeAsync(targetArticle, cancellationToken);
        await articleCommentQueryRepository.ChangeRangeAsync(targetArticle.Comments, cancellationToken);
        await articleCommentAnswerQueryRepository.ChangeRangeAsync(targetArticle.Comments.SelectMany(comment => comment.Answers), cancellationToken);
    }

    public Task AfterHandleAsync(ArticleInActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}