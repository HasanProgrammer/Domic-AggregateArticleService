using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Article.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.Domain.Category.Contracts.Interfaces;
using Karami.Domain.Category.Events;
using Karami.Domain.File.Contracts.Interfaces;

namespace Karami.UseCase.CategoryUseCase.Events;

public class DeleteCategoryConsumerEventBusHandler : IConsumerEventBusHandler<CategoryDeleted>
{
    private readonly IArticleQueryRepository              _articleQueryRepository;
    private readonly ICategoryQueryRepository             _categoryQueryRepository;
    private readonly IFileQueryRepository                 _fileQueryRepository;
    private readonly IArticleCommentQueryRepository       _articleCommentQueryRepository;
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public DeleteCategoryConsumerEventBusHandler(ICategoryQueryRepository categoryQueryRepository,
        IArticleQueryRepository articleQueryRepository, IFileQueryRepository fileQueryRepository,
        IArticleCommentQueryRepository articleCommentQueryRepository, 
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    )
    {
        _articleQueryRepository              = articleQueryRepository;
        _categoryQueryRepository             = categoryQueryRepository;
        _fileQueryRepository                 = fileQueryRepository;
        _articleCommentQueryRepository       = articleCommentQueryRepository;
        _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;
    }
    
    [WithTransaction]
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}|{Cache.AggregateArticles}")]
    public void Handle(CategoryDeleted @event)
    {
        var targetCategory = _categoryQueryRepository.FindById(@event.Id);

        if (targetCategory is not null)
        {
            targetCategory.IsDeleted   = IsDeleted.Delete;
            targetCategory.UpdatedBy   = @event.UpdatedBy;
            targetCategory.UpdatedRole = @event.UpdatedRole;
            targetCategory.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetCategory.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

            _categoryQueryRepository.Change(targetCategory);

            var articles =
                _articleQueryRepository.FindAllEagerLoadingByCategoryId(@event.Id);

            foreach (var article in articles)
            {
                foreach (var file in article.Files)
                    _fileQueryRepository.Remove(file);
                
                article.IsDeleted             = IsDeleted.Delete;
                article.UpdatedBy             = @event.UpdatedBy;
                article.UpdatedRole           = @event.UpdatedRole;
                article.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                article.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
                    
                _articleQueryRepository.Change(article);

                foreach (var comment in article.Comments)
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
}