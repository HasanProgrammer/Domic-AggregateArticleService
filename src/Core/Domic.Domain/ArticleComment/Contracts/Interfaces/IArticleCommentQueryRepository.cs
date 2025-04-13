using System.Linq.Expressions;
using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.ArticleComment.Contracts.Interfaces;

public interface IArticleCommentQueryRepository : IQueryRepository<Entities.ArticleCommentQuery, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="projection"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public new Task<List<TViewModel>> FindAllByProjectionAsync<TViewModel>(
        Expression<Func<Entities.ArticleCommentQuery, TViewModel>> projection, CancellationToken cancellationToken
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="comments"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ChangeRangeAsync(IEnumerable<Entities.ArticleCommentQuery> comments, CancellationToken cancellationToken);
}