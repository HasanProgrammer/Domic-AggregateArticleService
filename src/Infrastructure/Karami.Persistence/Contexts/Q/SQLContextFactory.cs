using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Karami.Persistence.Contexts.Q;

public class SQLContextFactory : IDesignTimeDbContextFactory<SQLContext>
{
    public SQLContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<SQLContext> builder = new DbContextOptionsBuilder<SQLContext>();
        
        builder.UseSqlServer("Somethings!");

        return new SQLContext(builder.Options);
    }
}