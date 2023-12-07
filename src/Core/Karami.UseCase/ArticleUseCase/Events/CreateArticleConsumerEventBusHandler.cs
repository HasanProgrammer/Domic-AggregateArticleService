using Karami.Core.Common.ClassConsts;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Article.Contracts.Interfaces;
using Karami.Domain.Article.Entities;
using Karami.Domain.Article.Events;
using Karami.Domain.File.Contracts.Interfaces;
using Karami.Domain.File.Entities;

namespace Karami.UseCase.CategoryUseCase.Events;

public class CreateArticleConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCreated>
{
    private readonly IFileQueryRepository    _fileQueryRepository;
    private readonly IArticleQueryRepository _articleQueryRepository;

    public CreateArticleConsumerEventBusHandler(IArticleQueryRepository articleQueryRepository,
        IFileQueryRepository fileQueryRepository
    )
    {
        _fileQueryRepository    = fileQueryRepository;
        _articleQueryRepository = articleQueryRepository;
    }
    
    [WithTransaction]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public void Handle(ArticleCreated @event)
    {
        var targetArticle = _articleQueryRepository.FindById(@event.Id);

        if (targetArticle is null)
        {
            var newArticle = new ArticleQuery {
                Id                    = @event.Id                    ,
                UserId                = @event.UserId                ,
                CategoryId            = @event.CategoryId            ,
                Title                 = @event.Title                 ,
                Summary               = @event.Summary               ,
                Body                  = @event.Body                  ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate ,
                UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate ,
                UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate
            };

            var newFile = new FileQuery {
                Id                    = @event.FileId                ,
                ArticleId             = @event.Id                    ,
                Path                  = @event.FilePath              ,
                Name                  = @event.FileName              ,
                Extension             = @event.FileExtension         ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate ,
                UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate ,
                UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate
            };
        
            _articleQueryRepository.Add(newArticle);
            _fileQueryRepository.Add(newFile);
        }
    }
}