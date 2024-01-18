using Karami.Core.Persistence.Configs;
using Karami.Domain.ArticleComment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.Q;

public class ArticleCommentQueryConfig : BaseEntityQueryConfig<ArticleCommentQuery, string>
{
    public override void Configure(EntityTypeBuilder<ArticleCommentQuery> builder)
    {
        builder.ToTable("ArticleComments");
        
        base.Configure(builder);
        
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