using Karami.Core.Domain.Enumerations;
using Karami.Domain.Category.Entities;
using Karami.Domain.File.Entities;
using Karami.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karami.Persistence.Configs.Q;

public class UserQueryConfig : IEntityTypeConfiguration<UserQuery>
{
    public void Configure(EntityTypeBuilder<UserQuery> builder)
    {
        //PrimaryKey
        
        builder.HasKey(user => user.Id);

        builder.ToTable("Users");
        
        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.Property(user => user.IsDeleted)
               .HasConversion(new EnumToNumberConverter<IsDeleted , int>());
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasMany(user => user.Articles)
               .WithOne(article => article.User)
               .HasForeignKey(article => article.UserId);
        
        /*-----------------------------------------------------------*/
        
        //Query

        builder.HasQueryFilter(user => user.IsDeleted == IsDeleted.UnDelete);
    }
}