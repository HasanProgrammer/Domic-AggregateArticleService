using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Domain.Category.Entities;

namespace Karami.Domain.Category.Contracts.Interfaces;

public interface ICategoryQueryRepository : IQueryRepository<CategoryQuery, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public CategoryQuery FindByName(string name) => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<CategoryQuery> FindByNameAsync(string name, CancellationToken cancellationToken) 
        => throw new NotImplementedException();
}