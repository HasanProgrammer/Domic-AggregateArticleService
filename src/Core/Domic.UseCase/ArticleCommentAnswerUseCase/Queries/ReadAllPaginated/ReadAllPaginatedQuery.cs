using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQuery : PaginatedQuery, IQuery<PaginatedCollection<ArticleCommentAnswersViewModel>>
{
    
}