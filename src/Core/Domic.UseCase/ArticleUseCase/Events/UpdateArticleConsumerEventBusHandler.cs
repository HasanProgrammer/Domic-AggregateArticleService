using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.Article.Events;
using Domic.Domain.File.Contracts.Interfaces;
using Domic.Domain.File.Entities;

namespace Domic.UseCase.CategoryUseCase.Events;

public class UpdateArticleConsumerEventBusHandler(
    IArticleQueryRepository articleQueryRepository,
    IFileQueryRepository fileQueryRepository
) : IConsumerEventBusHandler<ArticleUpdated>
{
    public Task BeforeHandleAsync(ArticleUpdated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public async Task HandleAsync(ArticleUpdated @event, CancellationToken cancellationToken)
    {
        var targetArticle = await articleQueryRepository.FindByIdEagerLoadingAsync(@event.Id, cancellationToken);

        targetArticle.CategoryId            = @event.CategoryId;
        targetArticle.Title                 = @event.Title;
        targetArticle.Summary               = @event.Summary;
        targetArticle.Body                  = @event.Body;
        targetArticle.UpdatedBy             = @event.UpdatedBy;
        targetArticle.UpdatedRole           = @event.UpdatedRole;
        targetArticle.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
        targetArticle.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

        if (@event.FileId is not null)
        {
            #region HardDelete Files

            await fileQueryRepository.RemoveRangeAsync(targetArticle.Files, cancellationToken);

            #endregion
            
            var newFile = new FileQuery {
                Id                    = @event.FileId                ,
                ArticleId             = @event.Id                    ,
                Path                  = @event.FilePath              ,
                Name                  = @event.FileName              ,
                Extension             = @event.FileExtension         ,
                CreatedBy             = @event.UpdatedBy             ,
                CreatedRole           = @event.UpdatedRole           ,
                CreatedAt_EnglishDate = @event.UpdatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.UpdatedAt_PersianDate
            };
                
            await fileQueryRepository.AddAsync(newFile, cancellationToken);
        }

        await articleQueryRepository.ChangeAsync(targetArticle, cancellationToken);
    }

    public Task AfterHandleAsync(ArticleUpdated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}