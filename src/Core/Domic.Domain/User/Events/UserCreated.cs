using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.User.Events;

[MessageBroker(Queue = Broker.AggregateArticle_User_Queue)]
public class UserCreated : CreateDomainEvent<string>
{
    public required string FirstName { get; init; }
    public required string LastName  { get; init; }
    public required bool IsActive    { get; init; }
}