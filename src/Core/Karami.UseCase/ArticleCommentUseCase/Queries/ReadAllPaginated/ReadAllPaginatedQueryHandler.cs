using Karami.Core.Common.ClassExtensions;
using Karami.Core.Common.ClassHelpers;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.UseCase.ArticleCommentUseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler : IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<ArticleCommentsViewModel>>
{
    private readonly ICacheService _cacheService;

    public ReadAllPaginatedQueryHandler(ICacheService cacheService) => _cacheService = cacheService;

    [WithValidation]
    public async Task<PaginatedCollection<ArticleCommentsViewModel>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        var pageNumber   = Convert.ToInt32(query.PageNumber);
        var countPerPage = Convert.ToInt32(query.CountPerPage);
        
        var articles = await _cacheService.GetAsync<IEnumerable<ArticleCommentsViewModel>>(cancellationToken);

        return articles.ToPaginatedCollection(articles.Count(), countPerPage, pageNumber, paginating: true);
    }
}