using Karami.Domain.User.Entities;
using Karami.Persistence.Contexts.Q;

namespace Karami.Infrastructure.Extensions.Q;

public static class SQLContextExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public static void Seed(this SQLContext context)
    {
        #region Article Seeder

        const string userId = "66a16cff-449d-4b3e-b8b0-b46a1cd2df44";

        var newUser = new UserQuery {
            Id        = userId  ,
            FirstName = "Hasan" ,
            LastName  = "Karami Moheb"                   
        };

        context.Users.Add(newUser);
        
        #endregion

        context.SaveChanges();
    }
}