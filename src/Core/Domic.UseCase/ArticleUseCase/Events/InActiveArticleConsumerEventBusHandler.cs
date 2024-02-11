using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.Article.Events;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.CategoryUseCase.Events;

public class InActiveArticleConsumerEventBusHandler : IConsumerEventBusHandler<ArticleInActived>
{
    private readonly IArticleQueryRepository              _articleQueryRepository;
    private readonly IArticleCommentQueryRepository       _articleCommentQueryRepository;
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public InActiveArticleConsumerEventBusHandler(IArticleQueryRepository articleQueryRepository,
        IArticleCommentQueryRepository articleCommentQueryRepository, 
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    )
    {
        _articleQueryRepository              = articleQueryRepository;
        _articleCommentQueryRepository       = articleCommentQueryRepository;
        _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;
    }

    [WithTransaction]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public void Handle(ArticleInActived @event)
    {
        var targetArticle = _articleQueryRepository.FindByIdEagerLoading(@event.Id);

        targetArticle.IsActive              = IsActive.InActive;
        targetArticle.UpdatedBy             = @event.UpdatedBy;
        targetArticle.UpdatedRole           = @event.UpdatedRole;
        targetArticle.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
        targetArticle.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

        _articleQueryRepository.Change(targetArticle);
        
        foreach (var comment in targetArticle.Comments)
        {
            comment.IsActive              = IsActive.InActive;
            comment.UpdatedBy             = @event.UpdatedBy;
            comment.UpdatedRole           = @event.UpdatedRole;
            comment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            comment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentQueryRepository.Change(comment);

            foreach (var answer in comment.Answers)
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