using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Events;
using Domic.Domain.File.Contracts.Interfaces;
using Domic.Domain.File.Entities;

namespace Domic.UseCase.CategoryUseCase.Events;

public class DeleteCategoryConsumerEventBusHandler(
    ICategoryQueryRepository categoryQueryRepository,
    IArticleQueryRepository articleQueryRepository,
    IFileQueryRepository fileQueryRepository,
    IArticleCommentQueryRepository articleCommentQueryRepository,
    IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
) : IConsumerEventBusHandler<CategoryDeleted>
{
    public Task BeforeHandleAsync(CategoryDeleted @event, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}|{Cache.AggregateArticles}")]
    public async Task HandleAsync(CategoryDeleted @event, CancellationToken cancellationToken)
    {
        var targetCategory = await categoryQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        if (targetCategory is not null)
        {
            targetCategory.IsDeleted   = IsDeleted.Delete;
            targetCategory.UpdatedBy   = @event.UpdatedBy;
            targetCategory.UpdatedRole = @event.UpdatedRole;
            targetCategory.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetCategory.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

            await categoryQueryRepository.ChangeAsync(targetCategory, cancellationToken);

            var articles =
                await articleQueryRepository.FindAllEagerLoadingByCategoryIdAsync(@event.Id, cancellationToken);

            var files = new List<FileQuery>();

            foreach (var article in articles)
            {
                files.AddRange(article.Files);
                
                article.IsDeleted             = IsDeleted.Delete;
                article.UpdatedBy             = @event.UpdatedBy;
                article.UpdatedRole           = @event.UpdatedRole;
                article.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                article.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

                foreach (var comment in article.Comments)
                {
                    comment.IsDeleted             = IsDeleted.Delete;
                    comment.UpdatedBy             = @event.UpdatedBy;
                    comment.UpdatedRole           = @event.UpdatedRole;
                    comment.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                    comment.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

                    foreach (var answer in comment.Answers)
                    {
                        answer.IsDeleted             = IsDeleted.Delete;
                        answer.UpdatedBy             = @event.UpdatedBy;
                        answer.UpdatedRole           = @event.UpdatedRole;
                        answer.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
                        answer.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
                    }
                }
            }

            if(files.Count > 0)
                await fileQueryRepository.RemoveRangeAsync(files, cancellationToken);

            await articleQueryRepository.ChangeRangeAsync(articles, cancellationToken);
            await articleCommentQueryRepository.ChangeRangeAsync(articles.SelectMany(article => article.Comments), cancellationToken);
            await articleCommentAnswerQueryRepository.ChangeRangeAsync(articles.SelectMany(article => article.Comments).SelectMany(comment => comment.Answers), cancellationToken);
        }
    }
    
    public Task AfterHandleAsync(CategoryDeleted @event, CancellationToken cancellationToken) 
        => Task.CompletedTask;
}