using System.Linq.Expressions;
using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projection"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public new Task<List<TViewModel>> FindAllByProjectionAsync<TViewModel>(
        Expression<Func<Entities.ArticleCommentAnswerQuery, TViewModel>> projection, CancellationToken cancellationToken
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="answers"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ChangeRangeAsync(IEnumerable<Entities.ArticleCommentAnswerQuery> answers, CancellationToken cancellationToken);
}