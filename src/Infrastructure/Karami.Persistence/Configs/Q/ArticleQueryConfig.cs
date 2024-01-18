using Karami.Core.Persistence.Configs;
using Karami.Domain.Article.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.Q;

public class ArticleQueryConfig : BaseEntityQueryConfig<ArticleQuery, string>
{
    public override void Configure(EntityTypeBuilder<ArticleQuery> builder)
    {
        builder.ToTable("Articles");
        
        base.Configure(builder);

        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasMany(article => article.Files).WithOne(file => file.Article).HasForeignKey(file => file.ArticleId);
    }
}