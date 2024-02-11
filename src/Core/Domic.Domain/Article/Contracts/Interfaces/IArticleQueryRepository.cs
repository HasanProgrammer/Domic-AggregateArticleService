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
    public List<ArticleQuery> FindAllEagerLoadingByCategoryId(string categoryId) => throw new NotImplementedException();
}