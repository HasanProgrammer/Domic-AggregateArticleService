using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs;
using Domic.UseCase.ArticleCommentUseCase.DTOs;
using Domic.UseCase.ArticleUseCase.DTOs;

namespace Domic.UseCase.ArticleUseCase.Caches;

public class ArticleInternalDistributedCache(IArticleQueryRepository articleQueryRepository) 
    : IInternalDistributedCacheHandler<List<ArticleDto>>
{
    [Config(Key = Cache.AggregateArticles, Ttl = 4 * 24 * 60)]
    public Task<List<ArticleDto>> SetAsync(CancellationToken cancellationToken) 
        => articleQueryRepository.FindAllByProjectionAsync<ArticleDto>(article =>
            new ArticleDto {
                Id                = article.Id      ,
                Title             = article.Title   ,
                Summary           = article.Summary ,
                Body              = article.Body    ,
                IsDeleted         = article.IsDeleted == IsDeleted.Delete ,
                IsActive          = article.IsActive == IsActive.Active   ,
                CreatedBy         = article.CreatedBy                     , 
                CreatedAt_Persian = article.CreatedAt_PersianDate         ,
                UpdatedAt_Persian = article.UpdatedAt_PersianDate         ,
                CreatedAt_English = article.CreatedAt_EnglishDate         ,
                UpdatedAt_English = article.UpdatedAt_EnglishDate         ,
                UserName          = $"{article.User.FirstName} {article.User.LastName}" ,
                CategoryId        = article.CategoryId    , 
                CategoryName      = article.Category.Name ,
                IndicatorImage    = article.Files.Any() ? article.Files.FirstOrDefault().Path : null ,
                Comments = article.Comments.Select(comment => new ArticleCommentDto {
                    Id                = comment.Id ,
                    CreatedByFullName = comment.User.FirstName + " " + comment.User.LastName ,
                    ArticleTitle      = comment.Article.Title               ,
                    Comment           = comment.Comment                     ,
                    IsActive          = comment.IsActive == IsActive.Active ,
                    CreatedAt         = comment.CreatedAt_PersianDate       ,
                    Answers = comment.Answers.Select(answer => new ArticleCommentAnswerDto {
                        Id                = answer.Id ,
                        CreatedBy         = answer.CreatedBy ,
                        CreatedByFullName = answer.User.FirstName + " " + answer.User.LastName ,
                        Answer            = answer.Answer                      ,
                        IsActive          = answer.IsActive == IsActive.Active ,
                        CreatedAt         = answer.CreatedAt_PersianDate       ,
                    }).ToList() 
                }).ToList()
            },
            cancellationToken
        );
}