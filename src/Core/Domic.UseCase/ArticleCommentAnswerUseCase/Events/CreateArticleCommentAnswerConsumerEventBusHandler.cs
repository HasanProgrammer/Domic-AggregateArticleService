using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Domain.ArticleCommentAnswer.Events;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Events;

public class CreateArticleCommentAnswerConsumerEventBusHandler(
    IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
) : IConsumerEventBusHandler<ArticleCommentAnswerCreated>
{
    public Task BeforeHandleAsync(ArticleCommentAnswerCreated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleCommentAnswers}|{Cache.AggregateArticles}")]
    public async Task HandleAsync(ArticleCommentAnswerCreated @event, CancellationToken cancellationToken)
    {
        var targetAnswer = await articleCommentAnswerQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        if (targetAnswer is null)
        {
            var newAnswer = new ArticleCommentAnswerQuery {
                Id                    = @event.Id                    ,
                CreatedBy             = @event.CreatedBy             ,
                CreatedRole           = @event.CreatedRole           , 
                CommentId             = @event.CommentId             ,
                Answer                = @event.Answer                ,  
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate
            };

            await articleCommentAnswerQueryRepository.AddAsync(newAnswer, cancellationToken);
        }
    }

    public Task AfterHandleAsync(ArticleCommentAnswerCreated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}