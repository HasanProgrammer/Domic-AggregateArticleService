using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Category.Events;

[EventConfig(Queue = Broker.AggregateArticle_Category_Queue)]
public class CategoryUpdated : UpdateDomainEvent<string>
{
    public string Name { get; set; }
}