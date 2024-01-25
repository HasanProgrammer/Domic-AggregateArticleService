using Karami.Core.Persistence.Configs;
using Karami.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.Q;

public class UserQueryConfig : BaseEntityQueryConfig<UserQuery, string>
{
    public override void Configure(EntityTypeBuilder<UserQuery> builder)
    {
        base.Configure(builder);

        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.ToTable("Users");
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasMany(user => user.Articles)
               .WithOne(article => article.User)
               .HasForeignKey(article => article.CreatedBy);
    }
}