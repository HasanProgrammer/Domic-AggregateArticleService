using Karami.Core.Common.ClassExtensions;
using Karami.Core.Common.ClassHelpers;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler : 
    IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<ArticleCommentAnswersViewModel>>
{
    private readonly ICacheService _cacheService;

    public ReadAllPaginatedQueryHandler(ICacheService cacheService) => _cacheService = cacheService;

    [WithValidation]
    public async Task<PaginatedCollection<ArticleCommentAnswersViewModel>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        var pageNumber   = Convert.ToInt32(query.PageNumber);
        var countPerPage = Convert.ToInt32(query.CountPerPage);
        
        var articles = await _cacheService.GetAsync<IEnumerable<ArticleCommentAnswersViewModel>>(cancellationToken);

        return articles.ToPaginatedCollection(articles.Count(), countPerPage, pageNumber, paginating: true);
    }
}