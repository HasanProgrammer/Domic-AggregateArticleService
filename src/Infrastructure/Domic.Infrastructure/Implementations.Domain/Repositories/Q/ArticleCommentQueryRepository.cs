using System.Linq.Expressions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class ArticleCommentQueryRepository : IArticleCommentQueryRepository
{
    private readonly SQLContext _sqlContext;

    public ArticleCommentQueryRepository(SQLContext sqlContext) => _sqlContext = sqlContext;
}

//Transaction
public partial class ArticleCommentQueryRepository
{
    public void Add(ArticleCommentQuery entity) => _sqlContext.Comments.Add(entity);

    public void Change(ArticleCommentQuery entity) => _sqlContext.Comments.Update(entity);
}

//Query
public partial class ArticleCommentQueryRepository
{
    public ArticleCommentQuery FindById(object id) => _sqlContext.Comments.FirstOrDefault(comment => comment.Id.Equals(id));

    public ArticleCommentQuery FindByIdEagerLoading(object id) =>
        _sqlContext.Comments.Where(comment => comment.Id.Equals(id))
                            .Include(comment => comment.Answers)
                            .FirstOrDefault();

    public async Task<IEnumerable<ArticleCommentQuery>> FindAllEagerLoadingAsync(CancellationToken cancellationToken)
        => await _sqlContext.Comments.Include(comment => comment.Answers)
                                     .Include(comment => comment.User)
                                     .AsNoTracking()
                                     .ToListAsync(cancellationToken);

    public async Task<IEnumerable<TViewModel>> FindAllEagerLoadingByProjectionAsync<TViewModel>(
        Expression<Func<ArticleCommentQuery, TViewModel>> projection, CancellationToken cancellationToken
    )
    {
        var query = await _sqlContext.Comments.Include(comment => comment.Answers)
                                              .Include(comment => comment.User)
                                              .AsNoTracking()
                                              .Select(projection)
                                              .ToListAsync(cancellationToken);

        return query;
    }
}