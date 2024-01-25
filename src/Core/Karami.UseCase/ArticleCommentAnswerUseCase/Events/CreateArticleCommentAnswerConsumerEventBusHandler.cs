using System.Data;
using Karami.Core.Common.ClassConsts;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Entities;
using Karami.Domain.ArticleCommentAnswer.Events;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Events;

public class CreateArticleCommentAnswerConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentAnswerCreated>
{
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public CreateArticleCommentAnswerConsumerEventBusHandler(
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    ) => _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;
    
    [WithTransaction(IsolationLevel = IsolationLevel.ReadUncommitted)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleCommentAnswers}|{Cache.AggregateArticles}")]
    public void Handle(ArticleCommentAnswerCreated @event)
    {
        var targetAnswer = _articleCommentAnswerQueryRepository.FindById(@event.Id);

        if (targetAnswer is null)
        {
            var newAnswer = new ArticleCommentAnswerQuery {
                Id                    = @event.Id                    ,
                CreatedBy             = @event.CreatedBy             ,
                CreatedRole           = @event.CreatedRole           , 
                CommentId             = @event.CommentId             ,
                Answer                = @event.Answer                ,  
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate
            };

            _articleCommentAnswerQueryRepository.Add(newAnswer);
        }
    }
}