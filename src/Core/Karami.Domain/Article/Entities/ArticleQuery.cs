#pragma warning disable CS0649

using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Domain.ArticleComment.Entities;
using Karami.Domain.Category.Entities;
using Karami.Domain.File.Entities;
using Karami.Domain.User.Entities;

namespace Karami.Domain.Article.Entities;

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