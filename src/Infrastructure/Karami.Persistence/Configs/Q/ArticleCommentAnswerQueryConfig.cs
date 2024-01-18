using Karami.Core.Persistence.Configs;
using Karami.Domain.ArticleCommentAnswer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.Q;

public class ArticleCommentAnswerQueryConfig : BaseEntityQueryConfig<ArticleCommentAnswerQuery, string>
{
    public override void Configure(EntityTypeBuilder<ArticleCommentAnswerQuery> builder)
    {
        builder.ToTable("ArticleCommentAnswers");
        
        base.Configure(builder);

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