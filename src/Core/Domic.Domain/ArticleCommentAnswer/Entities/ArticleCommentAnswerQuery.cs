#pragma warning disable CS0649

using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.User.Entities;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.ArticleCommentAnswer.Entities;

public class ArticleCommentAnswerQuery : EntityQuery<string>
{
    public string CommentId { get; set; }
    public string Answer    { get; set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public UserQuery User              { get; set; }
    public ArticleCommentQuery Comment { get; set; }
}