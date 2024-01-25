using System.Data;
using Karami.Core.Common.ClassConsts;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Events;

namespace Karami.UseCase.ArticleCommentUseCase.Events;

public class UpdateArticleCommentConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentUpdated>
{
    private readonly IArticleCommentQueryRepository _articleCommentQueryRepository;

    public UpdateArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository) 
        => _articleCommentQueryRepository = articleCommentQueryRepository;

    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    [WithTransaction(IsolationLevel = IsolationLevel.ReadUncommitted)]
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