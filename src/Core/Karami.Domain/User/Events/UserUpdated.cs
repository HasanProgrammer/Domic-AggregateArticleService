using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.User.Events;

[MessageBroker(Queue = Broker.AggregateArticle_User_Queue)]
public class UserUpdated : UpdateDomainEvent
{
    public string Id        { get; init; }
    public string FirstName { get; init; }
    public string LastName  { get; init; }
    public bool IsActive    { get; init; }
}