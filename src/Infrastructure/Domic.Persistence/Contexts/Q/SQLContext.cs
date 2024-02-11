using Domic.Persistence.Configs.Q;
using Domic.Domain.Article.Entities;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Domain.Category.Entities;
using Domic.Domain.File.Entities;
using Domic.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domic.Persistence.Contexts.Q;

/*Setting*/
public partial class SQLContext : DbContext
{
    public SQLContext(DbContextOptions<SQLContext> options) : base(options)
    {
        
    }
}

/*Entity*/
public partial class SQLContext
{
    public DbSet<UserQuery> Users { get; set; }
    public DbSet<FileQuery> Files { get; set; }
    public DbSet<ArticleQuery> Articles { get; set; }
    public DbSet<ArticleCommentQuery> Comments { get; set; }
    public DbSet<ArticleCommentAnswerQuery> Answers { get; set; }
    public DbSet<CategoryQuery> Categories { get; set; }
}

/*Config*/
public partial class SQLContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserQueryConfig());
        builder.ApplyConfiguration(new FileQueryConfig());
        builder.ApplyConfiguration(new ArticleQueryConfig());
        builder.ApplyConfiguration(new ArticleCommentQueryConfig());
        builder.ApplyConfiguration(new ArticleCommentAnswerQueryConfig());
        builder.ApplyConfiguration(new CategoryQueryConfig());
    }
}