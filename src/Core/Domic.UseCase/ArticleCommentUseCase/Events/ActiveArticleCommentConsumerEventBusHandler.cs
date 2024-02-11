using System.Data;
using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Events;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Events;

public class ActiveArticleCommentConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentActived>
{
    private readonly IArticleCommentQueryRepository       _articleCommentQueryRepository;
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public ActiveArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository,
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    )
    {
        _articleCommentQueryRepository       = articleCommentQueryRepository;
        _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;
    }

    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    [WithTransaction(IsolationLevel = IsolationLevel.ReadUncommitted)]
    public void Handle(ArticleCommentActived @event)
    {
        var targetComment = _articleCommentQueryRepository.FindByIdEagerLoading(@event.Id);

        if (targetComment is not null)
        {
            targetComment.IsActive              = IsActive.Active;
            targetComment.UpdatedBy             = @event.UpdatedBy;
            targetComment.UpdatedRole           = @event.UpdatedRole;
            targetComment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetComment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentQueryRepository.Change(targetComment);

            foreach (var answer in targetComment.Answers)
            {
                answer.IsActive              = IsActive.Active;
                answer.UpdatedBy             = @event.UpdatedBy;
                answer.UpdatedRole           = @event.UpdatedRole;
                answer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                answer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
                
                _articleCommentAnswerQueryRepository.Change(answer);
            }
        }
    }
}