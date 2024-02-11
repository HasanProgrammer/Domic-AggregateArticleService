using Domic.Domain.Article.Entities;
using Domic.Core.Domain.Contracts.Abstracts;

#pragma warning disable CS0649

namespace Domic.Domain.Category.Entities;

public class CategoryQuery : EntityQuery<string>
{
    public string Name { get; set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public ICollection<ArticleQuery> Articles { get; set; }
}