#pragma warning disable CS0649

using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.Category.Entities;
using Domic.Domain.File.Entities;
using Domic.Domain.User.Entities;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Article.Entities;

public class ArticleQuery : EntityQuery<string>
{
    public string CategoryId { get; set; }
    public string Title      { get; set; }
    public string Summary    { get; set; }
    public string Body       { get; set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public UserQuery User                            { get; set; }
    public CategoryQuery Category                    { get; set; }
    public ICollection<FileQuery> Files              { get; set; }
    public ICollection<ArticleCommentQuery> Comments { get; set; }
}