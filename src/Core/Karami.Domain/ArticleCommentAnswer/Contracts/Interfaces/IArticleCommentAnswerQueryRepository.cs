using Karami.Core.Domain.Contracts.Interfaces;

namespace Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;

public interface IArticleCommentAnswerQueryRepository : IQueryRepository<Entities.ArticleCommentAnswerQuery, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleCommentAnswerQuery>> FindAllByOwnerIdAsync(string ownerId,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleCommentAnswerQuery>> FindAllEagerLoadingByOwnerIdAsync(string ownerId,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}