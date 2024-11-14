using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Domain.ArticleCommentAnswer.Events;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Events;

public class CreateArticleCommentAnswerConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentAnswerCreated>
{
    private readonly IArticleCommentAnswerQueryRepository _articleCommentAnswerQueryRepository;

    public CreateArticleCommentAnswerConsumerEventBusHandler(
        IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
    ) => _articleCommentAnswerQueryRepository = articleCommentAnswerQueryRepository;

    [TransactionConfig(Type = TransactionType.Query)]
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

    public void AfterTransactionHandle(ArticleCommentAnswerCreated @event){}
}