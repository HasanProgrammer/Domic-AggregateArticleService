﻿using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;
using Domic.UseCase.ArticleCommentUseCase.DTOs.ViewModels;
using Domic.UseCase.ArticleUseCase.DTOs.ViewModels;

namespace Domic.UseCase.ArticleUseCase.Caches;

public class ArticlesEagerLoadingMemoryCache : IInternalDistributedCacheHandler<IEnumerable<ArticlesViewModel>>
{
    private readonly IArticleQueryRepository _articleQueryRepository;

    public ArticlesEagerLoadingMemoryCache(IArticleQueryRepository articleQueryRepository)
        => _articleQueryRepository = articleQueryRepository;

    [Config(Key = Cache.AggregateArticles, Ttl = 4 * 24 * 60)]
    public async Task<IEnumerable<ArticlesViewModel>> SetAsync(CancellationToken cancellationToken)
    {
        var articles = await _articleQueryRepository.FindAllEagerLoadingByProjectionAsync<ArticlesViewModel>(article =>
            new ArticlesViewModel {
                Id                = article.Id      ,
                Title             = article.Title   ,
                Summary           = article.Summary ,
                Body              = article.Body    ,
                IsDeleted         = article.IsDeleted == IsDeleted.Delete ,
                IsActive          = article.IsActive == IsActive.Active   ,
                CreatedAt_Persian = article.CreatedAt_PersianDate         ,
                UpdatedAt_Persian = article.UpdatedAt_PersianDate         ,
                CreatedAt_English = article.CreatedAt_EnglishDate         ,
                UpdatedAt_English = article.UpdatedAt_EnglishDate         ,
                UserName          = $"{article.User.FirstName} {article.User.LastName}" ,
                CategoryId        = article.CategoryId    , 
                CategoryName      = article.Category.Name ,
                IndicatorImage    = article.Files.Any() ? article.Files.FirstOrDefault().Path : null ,
                Comments = article.Comments.Select(comment => new ArticleCommentsViewModel {
                    Id            = comment.Id ,
                    OwnerFullName = comment.User.FirstName + " " + comment.User.LastName ,
                    ArticleTitle  = comment.Article.Title               ,
                    Comment       = comment.Comment                     ,
                    IsActive      = comment.IsActive == IsActive.Active ,
                    CreatedAt     = comment.CreatedAt_PersianDate       ,
                    Answers = comment.Answers.Select(answer => new ArticleCommentAnswersViewModel {
                        Id            = answer.Id ,
                        OwnerFullName = answer.User.FirstName + " " + answer.User.LastName ,
                        Answer        = answer.Answer                      ,
                        IsActive      = answer.IsActive == IsActive.Active ,
                        CreatedAt     = answer.CreatedAt_PersianDate       ,
                    }).ToList() 
                }).ToList()
            },
            cancellationToken
        );

        return articles;
    }
}