using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Caches;

public class ArticleCommentAnswersEagerLoadingMemoryCache(
    IArticleCommentAnswerQueryRepository articleCommentAnswerQueryRepository
) : IInternalDistributedCacheHandler<List<ArticleCommentAnswerDto>>
{
    [Config(Key = Cache.AggregateArticleCommentAnswers, Ttl = 4 * 24 * 60)]
    public Task<List<ArticleCommentAnswerDto>> SetAsync(CancellationToken cancellationToken) 
        => articleCommentAnswerQueryRepository.FindAllByProjectionAsync(answer =>
            new ArticleCommentAnswerDto {
                Id                = answer.Id                                          ,    
                CreatedBy         = answer.User.Id                                     ,
                CreatedByFullName = answer.User.FirstName + " " + answer.User.LastName ,
                ArticleTitle      = answer.Comment.Article.Title                       ,
                Answer            = answer.Answer                                      ,
                IsActive          = answer.IsActive == IsActive.Active                 ,
                CreatedAt         = answer.CreatedAt_PersianDate 
            },
            cancellationToken
        );
}