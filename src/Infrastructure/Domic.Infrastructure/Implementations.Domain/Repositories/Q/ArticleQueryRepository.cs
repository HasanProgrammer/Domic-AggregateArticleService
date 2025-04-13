using System.Linq.Expressions;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.Article.Entities;
using Domic.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class ArticleQueryRepository(SQLContext sqlContext) : IArticleQueryRepository;

//Transaction
public partial class ArticleQueryRepository
{
    public Task AddAsync(ArticleQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Articles.Add(entity);

        return Task.CompletedTask;
    }

    public Task ChangeAsync(ArticleQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Articles.Update(entity);

        return Task.CompletedTask;
    }

    public Task ChangeRangeAsync(IEnumerable<ArticleQuery> entities, CancellationToken cancellationToken)
    {
        sqlContext.Articles.UpdateRange(entities);

        return Task.CompletedTask;
    }
}

//Query
public partial class ArticleQueryRepository
{
    public Task<ArticleQuery> FindByIdAsync(object id, CancellationToken cancellationToken)
        => sqlContext.Articles.AsNoTracking().FirstOrDefaultAsync(article => article.Id == id as string, cancellationToken);

    public Task<ArticleQuery> FindByIdEagerLoadingAsync(object id, CancellationToken cancellationToken)
        => sqlContext.Articles.Where(article => article.Id == id as string)
                              .Include(article => article.Files)
                              .Include(article => article.Comments)
                              .ThenInclude(comment => comment.Answers)
                              .FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<ArticleQuery>> FindAllEagerLoadingAsync(CancellationToken cancellationToken)
        => await sqlContext.Articles.Include(article => article.Files)
                                     .Include(article => article.User)
                                     .Include(article => article.Category)
                                     .AsNoTracking().ToListAsync(cancellationToken);

    public async Task<IEnumerable<TViewModel>> FindAllEagerLoadingByProjectionAsync<TViewModel>(
        Expression<Func<ArticleQuery, TViewModel>> projection, CancellationToken cancellationToken
    )
    {
        var query = await sqlContext.Articles.Include(article => article.Files)
                                             .Include(article => article.User)
                                             .Include(article => article.Category)
                                             .Include(article => article.Comments)
                                             .ThenInclude(comment => comment.Answers)
                                             .AsNoTracking()
                                             .Select(projection)
                                             .ToListAsync(cancellationToken);

        return query;
    }

    public Task<List<ArticleQuery>> FindAllEagerLoadingByCategoryIdAsync(string categoryId, CancellationToken cancellationToken)
        => sqlContext.Articles.AsNoTracking()
                              .Where(article => article.CategoryId == categoryId)
                              .Include(article => article.Files)
                              .Include(article => article.Comments)
                              .ThenInclude(comment => comment.Answers)
                              .ToListAsync(cancellationToken);

    public Task<List<TViewModel>> FindAllByProjectionAsync<TViewModel>(
        Expression<Func<ArticleQuery, TViewModel>> projection, CancellationToken cancellationToken
    ) => sqlContext.Articles.AsNoTracking().Select(projection).ToListAsync(cancellationToken);
}