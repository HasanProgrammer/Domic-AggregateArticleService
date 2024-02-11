using Domic.Domain.Article.Entities;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Domain.User.Entities;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.ArticleComment.Entities;

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