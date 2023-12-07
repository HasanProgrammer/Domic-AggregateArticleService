using Karami.Domain.File.Contracts.Interfaces;
using Karami.Domain.File.Entities;
using Karami.Persistence.Contexts.Q;

namespace Karami.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class FileQueryRepository : IFileQueryRepository
{
    private readonly SQLContext _sqlContext;

    public FileQueryRepository(SQLContext sqlContext) => _sqlContext = sqlContext;
}

//Transaction
public partial class FileQueryRepository
{
    public void Add(FileQuery entity) => _sqlContext.Files.Add(entity);

    public void RemoveRange(IEnumerable<FileQuery> entities) => _sqlContext.Files.RemoveRange(entities);
}

//Query
public partial class FileQueryRepository
{
    public FileQuery FindById(object id) => _sqlContext.Files.FirstOrDefault(file => file.Id.Equals(id));

    public List<FileQuery> FindAllByArticleId(string articleId)
        => _sqlContext.Files.Where(file => file.ArticleId.Equals(articleId)).ToList();
}