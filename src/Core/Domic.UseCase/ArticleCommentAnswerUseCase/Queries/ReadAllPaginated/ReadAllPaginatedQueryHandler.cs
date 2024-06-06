using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;
using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler : 
    IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<ArticleCommentAnswersViewModel>>
{
    private readonly IInternalDistributedCacheMediator _distributedCacheMediator;

    public ReadAllPaginatedQueryHandler(IInternalDistributedCacheMediator distributedCacheMediator)
        => _distributedCacheMediator = distributedCacheMediator;

    [WithValidation]
    public async Task<PaginatedCollection<ArticleCommentAnswersViewModel>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        var pageNumber   = Convert.ToInt32(query.PageNumber);
        var countPerPage = Convert.ToInt32(query.CountPerPage);
        
        var articles = await _distributedCacheMediator.GetAsync<IEnumerable<ArticleCommentAnswersViewModel>>(cancellationToken);

        return articles.ToPaginatedCollection(articles.Count(), countPerPage, pageNumber, paginating: true);
    }
}