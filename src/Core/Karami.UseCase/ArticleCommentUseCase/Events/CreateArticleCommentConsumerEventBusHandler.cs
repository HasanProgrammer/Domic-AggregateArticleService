using System.Data;
using Karami.Core.Common.ClassConsts;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Entities;
using Karami.Domain.ArticleComment.Events;

namespace Karami.UseCase.ArticleCommentUseCase.Events;

public class CreateArticleCommentConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentCreated>
{
    private readonly IArticleCommentQueryRepository _articleCommentQueryRepository;

    public CreateArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository) 
        => _articleCommentQueryRepository = articleCommentQueryRepository;
    
    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    [WithTransaction(IsolationLevel = IsolationLevel.ReadUncommitted)]
    public void Handle(ArticleCommentCreated @event)
    {
        var targetComment = _articleCommentQueryRepository.FindById(@event.Id);

        if (targetComment is null)
        {
            var newComment = new ArticleCommentQuery {
                Id                    = @event.Id                    ,
                OwnerId               = @event.OwnerId               ,
                ArticleId             = @event.ArticleId             ,
                Comment               = @event.Comment               ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate ,
                UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate
            };

            _articleCommentQueryRepository.Add(newComment);
        }
    }
}