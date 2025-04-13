using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Entities;
using Domic.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class UserQueryRepository(SQLContext sqlContext) : IUserQueryRepository;

//Transaction
public partial class UserQueryRepository
{
    public Task AddAsync(UserQuery entity, CancellationToken cancellationToken) 
    {
        sqlContext.Users.Add(entity);

        return Task.CompletedTask;
    }

    public Task ChangeAsync(UserQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Users.Update(entity);

        return Task.CompletedTask;
    }
}

//Query
public partial class UserQueryRepository
{
    public Task<UserQuery> FindByIdAsync(object id, CancellationToken cancellationToken) 
        => sqlContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id as string, cancellationToken);
}