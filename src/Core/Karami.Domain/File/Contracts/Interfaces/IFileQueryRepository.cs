using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Domain.File.Entities;

namespace Karami.Domain.File.Contracts.Interfaces;

public interface IFileQueryRepository : IQueryRepository<FileQuery, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="articleId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<FileQuery> FindAllByArticleId(string articleId) => throw new NotImplementedException();
}