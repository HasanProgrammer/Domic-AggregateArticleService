using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleComment.Events;

namespace Domic.UseCase.ArticleCommentUseCase.Events;

public class CreateArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository) : IConsumerEventBusHandler<ArticleCommentCreated>
{
    public Task BeforeHandleAsync(ArticleCommentCreated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    public async Task HandleAsync(ArticleCommentCreated @event, CancellationToken cancellationToken)
    {
        var targetComment = await articleCommentQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        if (targetComment is null)
        {
            var newComment = new ArticleCommentQuery {
                Id                    = @event.Id                    ,
                ArticleId             = @event.ArticleId             ,
                Comment               = @event.Comment               ,
                CreatedBy             = @event.CreatedBy             ,
                CreatedRole           = @event.CreatedRole           ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate 
            };

            await articleCommentQueryRepository.AddAsync(newComment, cancellationToken);
        }
    }

    public Task AfterHandleAsync(ArticleCommentCreated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}