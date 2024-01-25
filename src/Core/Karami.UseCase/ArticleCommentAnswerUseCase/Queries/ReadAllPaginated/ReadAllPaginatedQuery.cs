using Karami.Core.Common.ClassHelpers;
using Karami.Core.UseCase.Contracts.Abstracts;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQuery : PaginatedQuery, IQuery<PaginatedCollection<ArticleCommentAnswersViewModel>>
{
    
}