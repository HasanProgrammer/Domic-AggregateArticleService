#pragma warning disable CS0649

using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Domain.ArticleComment.Entities;
using Karami.Domain.User.Entities;

namespace Karami.Domain.ArticleCommentAnswer.Entities;

public class ArticleCommentAnswerQuery : EntityQuery<string>
{
    public string OwnerId   { get; set; }
    public string CommentId { get; set; }
    public string Answer    { get; set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public UserQuery User              { get; set; }
    public ArticleCommentQuery Comment { get; set; }
}