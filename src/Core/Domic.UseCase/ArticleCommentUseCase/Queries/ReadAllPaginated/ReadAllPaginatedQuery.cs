using Domic.UseCase.ArticleCommentUseCase.DTOs;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQuery : PaginatedQuery, IQuery<PaginatedCollection<ArticleCommentDto>>
{
    public string UserId     { get; set; }
    public string SearchText { get; set; }
    public bool IsActive     { get; set; } = true;
}