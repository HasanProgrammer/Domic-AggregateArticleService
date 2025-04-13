using Domic.Domain.File.Contracts.Interfaces;
using Domic.Domain.File.Entities;
using Domic.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class FileQueryRepository(SQLContext sqlContext) : IFileQueryRepository;

//Transaction
public partial class FileQueryRepository
{
    public void Add(FileQuery entity) => sqlContext.Files.Add(entity);

    public Task AddAsync(FileQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Files.Add(entity);

        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(IEnumerable<FileQuery> entities, CancellationToken cancellationToken)
    {
        sqlContext.Files.RemoveRange(entities);

        return Task.CompletedTask;
    }
}

//Query
public partial class FileQueryRepository
{
    public Task<FileQuery> FindByIdAsync(object id, CancellationToken cancellationToken)
        => sqlContext.Files.AsNoTracking().FirstOrDefaultAsync(file => file.Id == id as string, cancellationToken);

    public Task<List<FileQuery>> FindAllByArticleIdAsync(string articleId, CancellationToken cancellationToken)
        => sqlContext.Files.AsNoTracking()
                           .Where(file => file.ArticleId == articleId)
                           .ToListAsync(cancellationToken);
}