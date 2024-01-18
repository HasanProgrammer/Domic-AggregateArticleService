using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.Category.Events;

[MessageBroker(Queue = Broker.AggregateArticle_Category_Queue)]
public class CategoryUpdated : UpdateDomainEvent<string>
{
    public string Name { get; set; }
}