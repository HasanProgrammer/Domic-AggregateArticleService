using Karami.Core.Domain.Enumerations;
using Karami.Domain.Category.Entities;
using Karami.Domain.File.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karami.Persistence.Configs.Q;

public class CategoryQueryConfig : IEntityTypeConfiguration<CategoryQuery>
{
    public void Configure(EntityTypeBuilder<CategoryQuery> builder)
    {
        //PrimaryKey
        
        builder.HasKey(category => category.Id);

        builder.ToTable("Categories");
        
        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.Property(category => category.IsDeleted)
               .HasConversion(new EnumToNumberConverter<IsDeleted , int>());
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasMany(category => category.Articles)
               .WithOne(article => article.Category)
               .HasForeignKey(article => article.CategoryId);
        
        /*-----------------------------------------------------------*/
        
        //Query

        builder.HasQueryFilter(category => category.IsDeleted == IsDeleted.UnDelete);
    }
}