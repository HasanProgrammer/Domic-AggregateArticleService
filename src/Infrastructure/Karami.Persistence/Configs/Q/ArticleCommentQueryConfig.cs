using Karami.Core.Domain.Enumerations;
using Karami.Domain.ArticleComment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karami.Persistence.Configs.Q;

public class ArticleCommentQueryConfig : IEntityTypeConfiguration<ArticleCommentQuery>
{
    public void Configure(EntityTypeBuilder<ArticleCommentQuery> builder)
    {
        //PrimaryKey
        
        builder.HasKey(article => article.Id);

        builder.ToTable("ArticleComments");

        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.Property(article => article.IsActive)
               .HasConversion(new EnumToNumberConverter<IsActive , int>());
        
        builder.Property(article => article.IsDeleted)
               .HasConversion(new EnumToNumberConverter<IsDeleted , int>());
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasOne(comment => comment.Article)
               .WithMany(article => article.Comments)
               .HasForeignKey(comment => comment.ArticleId);
        
        builder.HasOne(comment => comment.User)
               .WithMany(user => user.Comments)
               .HasForeignKey(comment => comment.OwnerId);

        /*-----------------------------------------------------------*/
        
        //Query

        builder.HasQueryFilter(article => article.IsDeleted == IsDeleted.UnDelete);
    }
}