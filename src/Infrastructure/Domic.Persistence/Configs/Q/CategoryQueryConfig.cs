using Domic.Core.Persistence.Configs;
using Domic.Domain.Category.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.Q;

public class CategoryQueryConfig : BaseEntityQueryConfig<CategoryQuery, string>
{
    public override void Configure(EntityTypeBuilder<CategoryQuery> builder)
    {
        base.Configure(builder);

        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.ToTable("Categories");
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasMany(category => category.Articles)
               .WithOne(article => article.Category)
               .HasForeignKey(article => article.CategoryId);
    }
}