using System.Linq.Expressions;
using Domic.Domain.Article.Contracts.Interfaces;
using Domic.Domain.Article.Entities;
using Domic.Domain.Article.Events;
using Domic.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class ArticleQueryRepository : IArticleQueryRepository
{
    private readonly SQLContext _sqlContext;

    public ArticleQueryRepository(SQLContext sqlContext) => _sqlContext = sqlContext;
}

//Transaction
public partial class ArticleQueryRepository
{
    public void Add(ArticleQuery entity) => _sqlContext.Articles.Add(entity);

    public void Change(ArticleQuery entity) => _sqlContext.Articles.Update(entity);
}

//Query
public partial class ArticleQueryRepository
{
    public ArticleQuery FindById(object id) => _sqlContext.Articles.FirstOrDefault(article => article.Id.Equals(id));

    public ArticleQuery FindByIdEagerLoading(object id) =>
        _sqlContext.Articles.Where(article => article.Id.Equals(id))
                            .Include(article => article.Files)
                            .Include(article => article.Comments)
                            .ThenInclude(comment => comment.Answers)
                            .FirstOrDefault();

    public async Task<IEnumerable<ArticleQuery>> FindAllEagerLoadingAsync(CancellationToken cancellationToken)
        => await _sqlContext.Articles.Include(article => article.Files)
                                     .Include(article => article.User)
                                     .Include(article => article.Category)
                                     .AsNoTracking().ToListAsync(cancellationToken);

    public async Task<IEnumerable<TViewModel>> FindAllEagerLoadingByProjectionAsync<TViewModel>(
        Expression<Func<ArticleQuery, TViewModel>> projection, CancellationToken cancellationToken
    )
    {
        var query = await _sqlContext.Articles.Include(article => article.Files)
                                              .Include(article => article.User)
                                              .Include(article => article.Category)
                                              .Include(article => article.Comments)
                                              .ThenInclude(comment => comment.Answers)
                                              .AsNoTracking()
                                              .Select(projection)
                                              .ToListAsync(cancellationToken);

        return query;
    }

    public List<ArticleQuery> FindAllEagerLoadingByCategoryId(string categoryId)
        => _sqlContext.Articles.Where(article => article.CategoryId.Equals(categoryId))
                               .Include(article => article.Files)
                               .Include(article => article.Comments)
                               .ThenInclude(comment => comment.Answers)
                               .ToList();
}