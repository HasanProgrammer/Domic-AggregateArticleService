using System.Linq.Expressions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class ArticleCommentQueryRepository(SQLContext sqlContext) : IArticleCommentQueryRepository;

//Transaction
public partial class ArticleCommentQueryRepository
{
    public Task AddAsync(ArticleCommentQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Comments.Add(entity);

        return Task.CompletedTask;
    }

    public Task ChangeAsync(ArticleCommentQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Comments.Update(entity);

        return Task.CompletedTask;
    }

    public Task ChangeRangeAsync(IEnumerable<ArticleCommentQuery> entities, CancellationToken cancellationToken)
    {
        sqlContext.Comments.UpdateRange(entities);

        return Task.CompletedTask;
    }
}

//Query
public partial class ArticleCommentQueryRepository
{
    public Task<ArticleCommentQuery> FindByIdAsync(object id, CancellationToken cancellationToken)
        => sqlContext.Comments.AsNoTracking().FirstOrDefaultAsync(comment => comment.Id == id as string, cancellationToken);

    public Task<List<TViewModel>> FindAllByProjectionAsync<TViewModel>(
        Expression<Func<ArticleCommentQuery, TViewModel>> projection, CancellationToken cancellationToken
    ) => sqlContext.Comments.AsNoTracking()
                            .Select(projection)
                            .ToListAsync(cancellationToken);
}