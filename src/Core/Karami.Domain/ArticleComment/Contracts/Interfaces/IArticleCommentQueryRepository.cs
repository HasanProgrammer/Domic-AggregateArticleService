using Karami.Core.Domain.Contracts.Interfaces;

namespace Karami.Domain.ArticleComment.Contracts.Interfaces;

public interface IArticleCommentQueryRepository : IQueryRepository<Entities.ArticleCommentQuery, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleCommentQuery>> FindAllByArticleIdAsync(string articleId, 
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleCommentQuery>> FindAllEagerLoadingByArticleIdAsync(string articleId, 
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleCommentQuery>> FindAllByOwnerIdAsync(string ownerId, 
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleCommentQuery>> FindAllEagerLoadingByOwnerIdAsync(string ownerId, 
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}