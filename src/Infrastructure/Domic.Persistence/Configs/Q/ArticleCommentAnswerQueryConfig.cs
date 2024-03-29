using Domic.Core.Persistence.Configs;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.Q;

public class ArticleCommentAnswerQueryConfig : BaseEntityQueryConfig<ArticleCommentAnswerQuery, string>
{
    public override void Configure(EntityTypeBuilder<ArticleCommentAnswerQuery> builder)
    {
        base.Configure(builder);

        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.ToTable("ArticleCommentAnswers");
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasOne(answer => answer.Comment)
               .WithMany(comment => comment.Answers)
               .HasForeignKey(answer => answer.CommentId);
        
        builder.HasOne(answer => answer.User)
               .WithMany(user => user.Answers)
               .HasForeignKey(answer => answer.CreatedBy);
    }
}