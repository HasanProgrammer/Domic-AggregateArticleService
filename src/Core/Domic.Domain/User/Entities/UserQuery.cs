#pragma warning disable CS0649

using Domic.Domain.Article.Entities;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.User.Entities;

public class UserQuery : EntityQuery<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public ICollection<ArticleQuery> Articles { get; set; }
    public ICollection<ArticleCommentQuery> Comments { get; set; }
    public ICollection<ArticleCommentAnswerQuery> Answers { get; set; }
}