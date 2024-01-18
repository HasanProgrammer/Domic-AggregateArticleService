using Karami.Core.Persistence.Configs;
using Karami.Domain.File.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.Q;

public class FileQueryConfig : BaseEntityQueryConfig<FileQuery, string>
{
    public override void Configure(EntityTypeBuilder<FileQuery> builder)
    {
        builder.ToTable("Files");
        
        base.Configure(builder);

        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasOne(file => file.Article).WithMany(article => article.Files).HasForeignKey(file => file.ArticleId);
    }
}