using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.ArticleCommentAnswer.Events;

[MessageBroker(Queue = Broker.AggregateArticle_ArticleCommentAnswer_Queue)]
public class ArticleCommentAnswerUpdated : UpdateDomainEvent<string>
{
    public string Answer { get; init; }
}