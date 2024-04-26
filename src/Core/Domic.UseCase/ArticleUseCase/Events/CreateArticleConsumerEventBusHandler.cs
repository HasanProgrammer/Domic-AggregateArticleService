using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.Article.Entities;
using Domic.Domain.Article.Events;
using Domic.Domain.File.Contracts.Interfaces;
using Domic.Domain.File.Entities;

namespace Domic.UseCase.CategoryUseCase.Events;

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
    
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public void Handle(ArticleCreated @event)
    {
        var targetArticle = _articleQueryRepository.FindById(@event.Id);

        if (targetArticle is null)
        {
            var newArticle = new ArticleQuery {
                Id                    = @event.Id                    ,
                CategoryId            = @event.CategoryId            ,
                CreatedRole           = @event.CreatedRole           ,
                CreatedBy             = @event.CreatedBy             ,
                Title                 = @event.Title                 ,
                Summary               = @event.Summary               ,
                Body                  = @event.Body                  ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate
            };

            var newFile = new FileQuery {
                Id                    = @event.FileId                ,
                ArticleId             = @event.Id                    ,
                CreatedRole           = @event.CreatedRole           ,
                CreatedBy             = @event.CreatedBy             ,
                Path                  = @event.FilePath              ,
                Name                  = @event.FileName              ,
                Extension             = @event.FileExtension         ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate
            };
        
            _articleQueryRepository.Add(newArticle);
            _fileQueryRepository.Add(newFile);
        }
    }
}