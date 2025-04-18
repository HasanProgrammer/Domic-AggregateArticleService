using Domic.UseCase.ArticleUseCase.DTOs;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleUseCase.Queries.ReadOne;

public class ReadOneQuery : IQuery<ArticleDto>
{
    public string Id { get; set; }
}