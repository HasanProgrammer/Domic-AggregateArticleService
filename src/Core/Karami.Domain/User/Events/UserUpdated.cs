using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.User.Events;

[MessageBroker(Queue = Broker.AggregateArticle_User_Queue)]
public class UserUpdated : UpdateDomainEvent<string>
{
    public required string FirstName { get; init; }
    public required string LastName  { get; init; }
    public required bool IsActive    { get; init; }
}