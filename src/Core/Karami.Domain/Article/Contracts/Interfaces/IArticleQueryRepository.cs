using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Domain.Article.Entities;

namespace Karami.Domain.Article.Contracts.Interfaces;

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