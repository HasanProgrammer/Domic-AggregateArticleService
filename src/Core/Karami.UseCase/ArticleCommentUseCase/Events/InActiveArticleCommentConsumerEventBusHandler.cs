using System.Data;
using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Events;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentUseCase.Events;

public class InActiveArticleCommentConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentInActived>
{
    private readonly IArticleCommentQueryRepository       _articleCommentQueryRepository;
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public InActiveArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository,
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    )
    {
        _articleCommentQueryRepository       = articleCommentQueryRepository;
        _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;
    }

    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    [WithTransaction(IsolationLevel = IsolationLevel.ReadUncommitted)]
    public void Handle(ArticleCommentInActived @event)
    {
        var targetComment = _articleCommentQueryRepository.FindByIdEagerLoading(@event.Id);

        if (targetComment is not null)
        {
            targetComment.IsActive              = IsActive.InActive;
            targetComment.UpdatedBy             = @event.UpdatedBy;
            targetComment.UpdatedRole           = @event.UpdatedRole;
            targetComment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetComment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentQueryRepository.Change(targetComment);

            foreach (var answer in targetComment.Answers)
            {
                answer.IsActive              = IsActive.InActive;
                answer.UpdatedBy             = @event.UpdatedBy;
                answer.UpdatedRole           = @event.UpdatedRole;
                answer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                answer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
                
                _articleCommentAnswerQueryRepository.Change(answer);
            }
        }
    }
}