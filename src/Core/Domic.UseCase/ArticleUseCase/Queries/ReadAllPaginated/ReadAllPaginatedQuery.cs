using Domic.UseCase.ArticleUseCase.DTOs;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQuery : PaginatedQuery, IQuery<PaginatedCollection<ArticleDto>>
{
    public string UserId     { get; set; }
    public string SearchText { get; set; }
    public bool IsActive     { get; set; } = true;
}