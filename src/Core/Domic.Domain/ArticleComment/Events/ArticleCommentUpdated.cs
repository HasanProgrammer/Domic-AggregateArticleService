using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.ArticleComment.Events;

[MessageBroker(Queue = Broker.AggregateArticle_ArticleComment_Queue)]
public class ArticleCommentUpdated : UpdateDomainEvent<string>
{
    public string Comment { get; init; }
}