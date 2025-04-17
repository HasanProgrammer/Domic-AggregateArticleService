using Domic.UseCase.ArticleUseCase.DTOs;
using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler(IInternalDistributedCacheMediator distributedCacheMediator) : 
    IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<ArticleDto>>
{
    [WithValidation]
    public async Task<PaginatedCollection<ArticleDto>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        var articles = await distributedCacheMediator.GetAsync<List<ArticleDto>>(cancellationToken);

        articles = articles.Where(article =>
            article.IsActive == query.IsActive &&
            ( string.IsNullOrEmpty(query.UserId) || article.CreatedBy == query.UserId ) &&
            ( string.IsNullOrEmpty(query.SearchText) || article.Title.Contains(query.SearchText) ) &&
            ( string.IsNullOrEmpty(query.SearchText) || article.UserName.Contains(query.SearchText) )
        ).ToList();

        return articles.ToPaginatedCollection(articles.Count, query.CountPerPage.Value, query.PageNumber.Value, paginating: true);
    }
}