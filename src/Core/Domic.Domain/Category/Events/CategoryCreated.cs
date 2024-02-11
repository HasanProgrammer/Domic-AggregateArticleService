using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Category.Events;

[MessageBroker(Queue = Broker.AggregateArticle_Category_Queue)]
public class CategoryCreated : CreateDomainEvent<string>
{
    public string Name { get; set; }
}