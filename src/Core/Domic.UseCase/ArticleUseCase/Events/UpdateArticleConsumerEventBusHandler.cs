using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.Article.Events;
using Domic.Domain.File.Contracts.Interfaces;
using Domic.Domain.File.Entities;

namespace Domic.UseCase.CategoryUseCase.Events;

public class UpdateArticleConsumerEventBusHandler : IConsumerEventBusHandler<ArticleUpdated>
{
    private readonly IFileQueryRepository    _fileQueryRepository;
    private readonly IArticleQueryRepository _articleQueryRepository;

    public UpdateArticleConsumerEventBusHandler(IArticleQueryRepository articleQueryRepository,
        IFileQueryRepository fileQueryRepository
    )
    {
        _fileQueryRepository    = fileQueryRepository;
        _articleQueryRepository = articleQueryRepository;
    }

    public void BeforeHandle(ArticleUpdated @event){}

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public void Handle(ArticleUpdated @event)
    {
        var targetArticle = _articleQueryRepository.FindByIdEagerLoading(@event.Id);

        targetArticle.CategoryId            = @event.CategoryId;
        targetArticle.UpdatedBy             = @event.UpdatedBy;
        targetArticle.UpdatedRole           = @event.UpdatedRole;
        targetArticle.Title                 = @event.Title;
        targetArticle.Summary               = @event.Summary;
        targetArticle.Body                  = @event.Body;
        targetArticle.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
        targetArticle.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

        if (@event.FileId is not null)
        {
            _fileQueryRepository.RemoveRange(targetArticle.Files);
            
            var newFile = new FileQuery {
                Id                    = @event.FileId                ,
                ArticleId             = @event.Id                    ,
                UpdatedBy             = @event.UpdatedBy             ,
                UpdatedRole           = @event.UpdatedRole           ,
                Path                  = @event.FilePath              ,
                Name                  = @event.FileName              ,
                Extension             = @event.FileExtension         ,
                CreatedAt_EnglishDate = @event.UpdatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.UpdatedAt_PersianDate ,
                UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate ,
                UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate
            };
                
            _fileQueryRepository.Add(newFile);
        }

        _articleQueryRepository.Change(targetArticle);
    }

    public void AfterHandle(ArticleUpdated @event){}
}