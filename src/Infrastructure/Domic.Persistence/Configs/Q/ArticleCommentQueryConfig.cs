using Domic.Core.Persistence.Configs;
using Domic.Domain.ArticleComment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.Q;

public class ArticleCommentQueryConfig : BaseEntityQueryConfig<ArticleCommentQuery, string>
{
    public override void Configure(EntityTypeBuilder<ArticleCommentQuery> builder)
    { 
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.ToTable("ArticleComments");
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasOne(comment => comment.Article)
               .WithMany(article => article.Comments)
               .HasForeignKey(comment => comment.ArticleId);
        
        builder.HasOne(comment => comment.User)
               .WithMany(user => user.Comments)
               .HasForeignKey(comment => comment.CreatedBy);
    }
}