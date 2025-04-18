#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

using Domic.UseCase.ArticleUseCase.DTOs;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.ArticleUseCase.Queries.ReadOne;

namespace Domic.UseCase.ArticleUseCase.Queries.ReadAllPaginated;

public class ReadOneQueryHandler : IQueryHandler<ReadOneQuery, ArticleDto>
{
    private readonly object _validationResult;
    
    [WithValidation]
    public Task<ArticleDto> HandleAsync(ReadOneQuery query, CancellationToken cancellationToken)
    {
        var targetArticle = _validationResult as ArticleDto;

        return Task.FromResult(targetArticle);
    }
}