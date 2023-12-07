using System.Linq.Expressions;
using Karami.Domain.ArticleComment.Entities;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Entities;
using Karami.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Karami.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class ArticleCommentAnswerQueryRepository : IArticleCommentAnswerQueryRepository
{
    private readonly SQLContext _sqlContext;

    public ArticleCommentAnswerQueryRepository(SQLContext sqlContext) => _sqlContext = sqlContext;
}

//Transaction
public partial class ArticleCommentAnswerQueryRepository
{
    public void Add(ArticleCommentAnswerQuery entity) => _sqlContext.Answers.Add(entity);

    public void Change(ArticleCommentAnswerQuery entity) => _sqlContext.Answers.Update(entity);
}

//Query
public partial class ArticleCommentAnswerQueryRepository
{
    public ArticleCommentAnswerQuery FindById(object id)
        => _sqlContext.Answers.FirstOrDefault(answer => answer.Id.Equals(id));

    public ArticleCommentAnswerQuery FindByIdEagerLoading(object id) =>
        _sqlContext.Answers.Where(answer => answer.Id.Equals(id)).Include(answer => answer.User).FirstOrDefault();

    public async Task<IEnumerable<ArticleCommentAnswerQuery>> FindAllEagerLoadingAsync(
        CancellationToken cancellationToken
    )
    => await _sqlContext.Answers.Include(comment => comment.User).AsNoTracking().ToListAsync(cancellationToken);

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