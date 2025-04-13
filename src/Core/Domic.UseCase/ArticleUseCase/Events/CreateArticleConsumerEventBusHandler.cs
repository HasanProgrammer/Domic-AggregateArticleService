using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.Article.Entities;
using Domic.Domain.Article.Events;
using Domic.Domain.File.Contracts.Interfaces;
using Domic.Domain.File.Entities;

namespace Domic.UseCase.CategoryUseCase.Events;

public class CreateArticleConsumerEventBusHandler(
    IArticleQueryRepository articleQueryRepository,
    IFileQueryRepository fileQueryRepository
) : IConsumerEventBusHandler<ArticleCreated>
{
    public Task BeforeHandleAsync(ArticleCreated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public async Task HandleAsync(ArticleCreated @event, CancellationToken cancellationToken)
    {
        var targetArticle = await articleQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        if (targetArticle is null)
        {
            var newArticle = new ArticleQuery {
                Id                    = @event.Id                    ,
                CategoryId            = @event.CategoryId            ,
                Title                 = @event.Title                 ,
                Summary               = @event.Summary               ,
                Body                  = @event.Body                  ,
                CreatedBy             = @event.CreatedBy             ,
                CreatedRole           = @event.CreatedRole           ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate
            };

            var newFile = new FileQuery {
                Id                    = @event.FileId                ,
                ArticleId             = @event.Id                    ,
                Path                  = @event.FilePath              ,
                Name                  = @event.FileName              ,
                Extension             = @event.FileExtension         ,
                CreatedBy             = @event.CreatedBy             ,
                CreatedRole           = @event.CreatedRole           ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate
            };
        
            await articleQueryRepository.AddAsync(newArticle, cancellationToken);
            await fileQueryRepository.AddAsync(newFile, cancellationToken);
        }
    }

    public Task AfterHandleAsync(ArticleCreated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}