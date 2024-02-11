using Domic.Domain.File.Entities;
using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.File.Contracts.Interfaces;

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