using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Domain.Article.Entities;
using Karami.Domain.ArticleCommentAnswer.Entities;
using Karami.Domain.User.Entities;

namespace Karami.Domain.ArticleComment.Entities;

public class ArticleCommentQuery : EntityQuery<string>
{
    public string ArticleId { get; set; }
    public string Comment   { get; set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public UserQuery User                                 { get; set; }
    public ArticleQuery Article                           { get; set; }
    public ICollection<ArticleCommentAnswerQuery> Answers { get; set; }
}