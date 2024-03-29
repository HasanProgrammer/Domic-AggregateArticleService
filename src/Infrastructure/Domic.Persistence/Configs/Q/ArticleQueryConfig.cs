using Domic.Core.Persistence.Configs;
using Domic.Domain.Article.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.Q;

public class ArticleQueryConfig : BaseEntityQueryConfig<ArticleQuery, string>
{
    public override void Configure(EntityTypeBuilder<ArticleQuery> builder)
    {
        base.Configure(builder);

        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.ToTable("Articles");
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasMany(article => article.Files).WithOne(file => file.Article).HasForeignKey(file => file.ArticleId);
    }
}