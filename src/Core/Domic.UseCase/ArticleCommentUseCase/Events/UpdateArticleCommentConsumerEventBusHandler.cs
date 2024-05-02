using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Events;

namespace Domic.UseCase.ArticleCommentUseCase.Events;

public class UpdateArticleCommentConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentUpdated>
{
    private readonly IArticleCommentQueryRepository _articleCommentQueryRepository;

    public UpdateArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository) 
        => _articleCommentQueryRepository = articleCommentQueryRepository;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    public void Handle(ArticleCommentUpdated @event)
    {
        var targetComment = _articleCommentQueryRepository.FindById(@event.Id);

        if (targetComment is not null)
        {
            targetComment.Comment               = @event.Comment;
            targetComment.UpdatedBy             = @event.UpdatedBy;
            targetComment.UpdatedRole           = @event.UpdatedRole;
            targetComment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetComment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentQueryRepository.Change(targetComment);
        }
    }
}