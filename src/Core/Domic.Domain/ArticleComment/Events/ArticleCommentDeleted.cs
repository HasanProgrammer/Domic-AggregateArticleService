using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.ArticleComment.Events;

[EventConfig(Queue = Broker.AggregateArticle_ArticleComment_Queue)]
public class ArticleCommentDeleted : UpdateDomainEvent<string>
{
    
}