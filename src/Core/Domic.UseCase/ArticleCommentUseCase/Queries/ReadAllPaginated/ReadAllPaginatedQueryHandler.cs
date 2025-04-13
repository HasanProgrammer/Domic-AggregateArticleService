using Domic.UseCase.ArticleCommentUseCase.DTOs;
using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler(IInternalDistributedCacheMediator distributedCacheMediator) : 
    IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<ArticleCommentDto>>
{
    [WithValidation]
    public async Task<PaginatedCollection<ArticleCommentDto>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {   
        var articles = await distributedCacheMediator.GetAsync<List<ArticleCommentDto>>(cancellationToken);

        articles = articles.Where(article => article.IsActive == query.IsActive &&
            ( string.IsNullOrEmpty(query.UserId) || article.CreatedBy == query.UserId ) &&
            ( string.IsNullOrEmpty(query.SearchText) || article.CreatedByFullName.Contains(query.SearchText) ) &&
            ( string.IsNullOrEmpty(query.SearchText) || article.ArticleTitle.Contains(query.SearchText) )
        ).ToList();

        return articles.ToPaginatedCollection(
            articles.Count(), query.CountPerPage.Value, query.PageNumber.Value, paginating: true
        );
    }
}