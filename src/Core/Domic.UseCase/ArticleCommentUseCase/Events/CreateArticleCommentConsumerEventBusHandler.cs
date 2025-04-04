﻿using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleComment.Events;

namespace Domic.UseCase.ArticleCommentUseCase.Events;

public class CreateArticleCommentConsumerEventBusHandler : IConsumerEventBusHandler<ArticleCommentCreated>
{
    private readonly IArticleCommentQueryRepository _articleCommentQueryRepository;

    public CreateArticleCommentConsumerEventBusHandler(IArticleCommentQueryRepository articleCommentQueryRepository) 
        => _articleCommentQueryRepository = articleCommentQueryRepository;

    public void BeforeHandle(ArticleCommentCreated @event){}

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticleComments}|{Cache.AggregateArticles}")]
    public void Handle(ArticleCommentCreated @event)
    {
        var targetComment = _articleCommentQueryRepository.FindById(@event.Id);

        if (targetComment is null)
        {
            var newComment = new ArticleCommentQuery {
                Id                    = @event.Id                    ,
                CreatedBy             = @event.CreatedBy             ,
                CreatedRole           = @event.CreatedRole           ,
                ArticleId             = @event.ArticleId             ,
                Comment               = @event.Comment               ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate ,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate 
            };

            _articleCommentQueryRepository.Add(newComment);
        }
    }

    public void AfterHandle(ArticleCommentCreated @event){}
}