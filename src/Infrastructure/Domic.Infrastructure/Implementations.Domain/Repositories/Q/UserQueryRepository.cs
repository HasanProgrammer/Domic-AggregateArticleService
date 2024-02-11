using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Entities;
using Domic.Persistence.Contexts.Q;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class UserQueryRepository : IUserQueryRepository
{
    private readonly SQLContext _sqlContext;

    public UserQueryRepository(SQLContext sqlContext) => _sqlContext = sqlContext;
}

//Transaction
public partial class UserQueryRepository
{
    public void Add(UserQuery entity) => _sqlContext.Users.Add(entity);

    public void Change(UserQuery entity) => _sqlContext.Users.Update(entity);
}

//Query
public partial class UserQueryRepository
{
    public UserQuery FindById(object id) => _sqlContext.Users.FirstOrDefault(user => user.Id.Equals(id));
}