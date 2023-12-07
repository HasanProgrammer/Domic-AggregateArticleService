using Karami.Core.Domain.Enumerations;
using Karami.Domain.ArticleCommentAnswer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karami.Persistence.Configs.Q;

public class ArticleCommentAnswerQueryConfig : IEntityTypeConfiguration<ArticleCommentAnswerQuery>
{
    public void Configure(EntityTypeBuilder<ArticleCommentAnswerQuery> builder)
    {
        //PrimaryKey
        
        builder.HasKey(article => article.Id);

        builder.ToTable("ArticleCommentAnswers");

        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.Property(article => article.IsActive)
               .HasConversion(new EnumToNumberConverter<IsActive , int>());
        
        builder.Property(article => article.IsDeleted)
               .HasConversion(new EnumToNumberConverter<IsDeleted , int>());
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasOne(answer => answer.Comment)
               .WithMany(comment => comment.Answers)
               .HasForeignKey(answer => answer.CommentId);
        
        builder.HasOne(answer => answer.User)
               .WithMany(user => user.Answers)
               .HasForeignKey(answer => answer.OwnerId);

        /*-----------------------------------------------------------*/
        
        //Query

        builder.HasQueryFilter(article => article.IsDeleted == IsDeleted.UnDelete);
    }
}