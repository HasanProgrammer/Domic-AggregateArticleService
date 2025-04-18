using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.UseCase.ArticleUseCase.DTOs;

namespace Domic.UseCase.ArticleUseCase.Queries.ReadOne;

public class ReadOneQueryValidator(IInternalDistributedCacheMediator distributedCacheMediator) : IValidator<ReadOneQuery>
{
    public async Task<object> ValidateAsync(ReadOneQuery input, CancellationToken cancellationToken)
    {
        var articles = await distributedCacheMediator.GetAsync<List<ArticleDto>>(cancellationToken);

        var targetArticle = articles.FirstOrDefault(article => article.Id == input.Id);
        
        if (targetArticle is null)
            throw new UseCaseException(
                string.Format("مقاله ای با شناسه {0} یافت نشد !", input.Id)
            );

        return targetArticle;
    }
}