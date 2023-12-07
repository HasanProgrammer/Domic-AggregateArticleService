using Karami.Core.Domain.Enumerations;
using Karami.Domain.Article.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karami.Persistence.Configs.Q;

public class ArticleQueryConfig : IEntityTypeConfiguration<ArticleQuery>
{
    public void Configure(EntityTypeBuilder<ArticleQuery> builder)
    {
        //PrimaryKey
        
        builder.HasKey(article => article.Id);

        builder.ToTable("Articles");

        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.Property(article => article.IsActive)
               .HasConversion(new EnumToNumberConverter<IsActive , int>());
        
        builder.Property(article => article.IsDeleted)
               .HasConversion(new EnumToNumberConverter<IsDeleted , int>());
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasMany(article => article.Files).WithOne(file => file.Article).HasForeignKey(file => file.ArticleId);
        
        /*-----------------------------------------------------------*/
        
        //Query

        builder.HasQueryFilter(article => article.IsDeleted == IsDeleted.UnDelete);
    }
}