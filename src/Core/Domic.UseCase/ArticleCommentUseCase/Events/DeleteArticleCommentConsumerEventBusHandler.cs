using System.Data;
using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Events;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Events;

public class DeleteArticleCommentConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentDeleted>
{
    private readonly IArticleCommentQueryRepository       _articleCommentQueryRepository;
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public DeleteArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository,
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    )
    {
        _articleCommentQueryRepository       = articleCommentQueryRepository;
        _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;
    }

    [TransactionIsolationLevel(Level = IsolationLevel.ReadUncommitted)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    public void Handle(ArticleCommentDeleted @event)
    {
        var targetComment = _articleCommentQueryRepository.FindByIdEagerLoading(@event.Id);

        if (targetComment is not null)
        {
            targetComment.IsDeleted             = IsDeleted.Delete;
            targetComment.UpdatedBy             = @event.UpdatedBy;
            targetComment.UpdatedRole           = @event.UpdatedRole;
            targetComment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetComment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentQueryRepository.Change(targetComment);

            foreach (var answer in targetComment.Answers)
            {
                answer.IsDeleted             = IsDeleted.Delete;
                answer.UpdatedBy             = @event.UpdatedBy;
                answer.UpdatedRole           = @event.UpdatedRole;
                answer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                answer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
                
                _articleCommentAnswerQueryRepository.Change(answer);
            }
        }
    }
}