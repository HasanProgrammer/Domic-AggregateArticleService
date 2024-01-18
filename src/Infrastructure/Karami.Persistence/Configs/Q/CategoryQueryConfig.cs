using Karami.Core.Persistence.Configs;
using Karami.Domain.Category.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.Q;

public class CategoryQueryConfig : BaseEntityQueryConfig<CategoryQuery, string>
{
    public override void Configure(EntityTypeBuilder<CategoryQuery> builder)
    {
        builder.ToTable("Categories");
        
        base.Configure(builder);

        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasMany(category => category.Articles)
               .WithOne(article => article.Category)
               .HasForeignKey(article => article.CategoryId);
    }
}