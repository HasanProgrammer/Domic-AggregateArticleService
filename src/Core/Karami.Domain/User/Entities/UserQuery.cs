#pragma warning disable CS0649

using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Enumerations;
using Karami.Domain.Article.Entities;
using Karami.Domain.ArticleComment.Entities;
using Karami.Domain.ArticleCommentAnswer.Entities;

namespace Karami.Domain.User.Entities;

public class UserQuery : BaseEntityQuery<string>
{
    public string FirstName  { get; set; }
    public string LastName   { get; set; }
    public IsActive IsActive { get; set; } = IsActive.Active;

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public ICollection<ArticleQuery> Articles             { get; set; }
    public ICollection<ArticleCommentQuery> Comments      { get; set; }
    public ICollection<ArticleCommentAnswerQuery> Answers { get; set; }
}