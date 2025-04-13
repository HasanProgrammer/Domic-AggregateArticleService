using System.Linq.Expressions;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class ArticleCommentAnswerQueryRepository(SQLContext sqlContext) : IArticleCommentAnswerQueryRepository;

//Transaction
public partial class ArticleCommentAnswerQueryRepository
{
    public Task AddAsync(ArticleCommentAnswerQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Answers.Add(entity);

        return Task.CompletedTask;
    }

    public Task ChangeAsync(ArticleCommentAnswerQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Answers.Update(entity);

        return Task.CompletedTask;
    }

    public Task ChangeRangeAsync(IEnumerable<ArticleCommentAnswerQuery> entities, CancellationToken cancellationToken)
    {
        sqlContext.Answers.UpdateRange(entities);

        return Task.CompletedTask;
    }
}

//Query
public partial class ArticleCommentAnswerQueryRepository
{
    public Task<ArticleCommentAnswerQuery> FindByIdAsync(object id, CancellationToken cancellationToken)
        => sqlContext.Answers.AsNoTracking().FirstOrDefaultAsync(answer => answer.Id == id as string, cancellationToken);

    public Task<List<TViewModel>> FindAllByProjectionAsync<TViewModel>(
        Expression<Func<ArticleCommentAnswerQuery, TViewModel>> projection, CancellationToken cancellationToken
    ) => sqlContext.Answers.AsNoTracking().Select(projection).ToListAsync(cancellationToken);
}