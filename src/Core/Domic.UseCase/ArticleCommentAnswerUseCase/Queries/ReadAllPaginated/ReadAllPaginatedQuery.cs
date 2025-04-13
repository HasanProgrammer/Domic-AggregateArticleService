using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQuery : PaginatedQuery, IQuery<PaginatedCollection<ArticleCommentAnswerDto>>
{
    public string UserId     { get; set; }
    public string SearchText { get; set; }
}