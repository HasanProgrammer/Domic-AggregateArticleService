using Domic.Domain.User.Entities;
using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.User.Contracts.Interfaces;

public interface IUserQueryRepository : IQueryRepository<UserQuery, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<UserQuery> FindByUsernameAsync(string username, CancellationToken cancellationToken) 
        => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<UserQuery> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken) 
        => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<UserQuery> FindByEmailAsync(string email, CancellationToken cancellationToken) 
        => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="email"></param>
    /// <param name="description"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<UserQuery>> SearchAsync(
        string username, string firstName, string lastName, string phoneNumber, string email, string description,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="email"></param>
    /// <param name="description"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<UserQuery>> SearchEagerLoadingAsync(
        string username, string firstName, string lastName, string phoneNumber, string email, string description,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}