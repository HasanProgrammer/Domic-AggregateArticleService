using Domic.Domain.Article.Entities;
using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.Article.Contracts.Interfaces;

public interface IArticleQueryRepository : IQueryRepository<ArticleQuery, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<ArticleQuery>> FindAllEagerLoadingByCategoryIdAsync(string categoryId, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="articles"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ChangeRangeAsync(IEnumerable<ArticleQuery> articles, CancellationToken cancellationToken);
}