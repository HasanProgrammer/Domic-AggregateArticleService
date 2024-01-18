﻿using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.User.Events;

[MessageBroker(Queue = Broker.AggregateArticle_User_Queue)]
public class UserActived : UpdateDomainEvent<string>
{
    public required string OwnerUsername { get; init; }
}