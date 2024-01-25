using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Article.Contracts.Interfaces;
using Karami.Domain.Article.Events;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.Domain.File.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Events;

public class DeleteArticleConsumerEventBusHandler : IConsumerEventBusHandler<ArticleDeleted>
{
    private readonly IFileQueryRepository                 _fileQueryRepository;
    private readonly IArticleQueryRepository              _articleQueryRepository;
    private readonly IArticleCommentQueryRepository       _articleCommentQueryRepository;
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public DeleteArticleConsumerEventBusHandler(IArticleQueryRepository articleQueryRepository,
        IFileQueryRepository fileQueryRepository, IArticleCommentQueryRepository articleCommentQueryRepository,
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    )
    {
        _fileQueryRepository                 = fileQueryRepository;
        _articleQueryRepository              = articleQueryRepository;
        _articleCommentQueryRepository       = articleCommentQueryRepository;
        _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;
    }
    
    [WithTransaction]
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}")]
    public void Handle(ArticleDeleted @event)
    {
        var targetArticle = _articleQueryRepository.FindByIdEagerLoading(@event.Id);

        if (targetArticle is not null)
        {
            #region HardDelete Files

            _fileQueryRepository.RemoveRange(targetArticle.Files);

            #endregion
            
            targetArticle.IsDeleted             = IsDeleted.Delete;
            targetArticle.UpdatedBy             = @event.UpdatedBy;
            targetArticle.UpdatedRole           = @event.UpdatedRole;
            targetArticle.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetArticle.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _articleQueryRepository.Change(targetArticle);
            
            foreach (var comment in targetArticle.Comments)
            {
                comment.IsDeleted             = IsDeleted.Delete;
                comment.UpdatedBy             = @event.UpdatedBy;
                comment.UpdatedRole           = @event.UpdatedRole;
                comment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                comment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
                _articleCommentQueryRepository.Change(comment);

                foreach (var answer in comment.Answers)
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
}