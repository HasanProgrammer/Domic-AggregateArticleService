using Karami.Core.Domain.Enumerations;
using Karami.Domain.File.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Karami.Persistence.Configs.Q;

public class FileQueryConfig : IEntityTypeConfiguration<FileQuery>
{
    public void Configure(EntityTypeBuilder<FileQuery> builder)
    {
        //PrimaryKey
        
        builder.HasKey(file => file.Id);

        builder.ToTable("Files");
        
        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.Property(file => file.IsDeleted)
               .HasConversion(new EnumToNumberConverter<IsDeleted , int>());
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasOne(file => file.Article).WithMany(article => article.Files).HasForeignKey(file => file.ArticleId);
        
        /*-----------------------------------------------------------*/
        
        //Query

        builder.HasQueryFilter(file => file.IsDeleted == IsDeleted.UnDelete);
    }
}