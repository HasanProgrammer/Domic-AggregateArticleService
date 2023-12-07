using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Domain.Article.Entities;

#pragma warning disable CS0649

namespace Karami.Domain.Category.Entities;

public class CategoryQuery : BaseEntityQuery<string>
{
    public string Name { get; set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public ICollection<ArticleQuery> Articles { get; set; }
}