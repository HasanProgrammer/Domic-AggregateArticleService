using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Article.Contracts.Interfaces;
using Karami.Domain.Article.Events;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Events;

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
        targetArticle.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
        targetArticle.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

        _articleQueryRepository.Change(targetArticle);
        
        foreach (var comment in targetArticle.Comments)
        {
            comment.IsActive              = IsActive.InActive;
            comment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            comment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleCommentQueryRepository.Change(comment);

            foreach (var answer in comment.Answers)
            {
                answer.IsActive              = IsActive.InActive;
                answer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                answer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
                
                _articleCommentAnswerQueryRepository.Change(answer);
            }
        }
    }
}