using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.ArticleCommentAnswer.Events;

[MessageBroker(Queue = Broker.AggregateArticle_ArticleCommentAnswer_Queue)]
public class ArticleCommentAnswerActived : UpdateDomainEvent
{
    public string Id { get; init; }
}