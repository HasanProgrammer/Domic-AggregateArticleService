﻿using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.Article.Events;

[MessageBroker(Queue = Broker.AggregateArticle_Article_Queue)]
public class ArticleInActived : UpdateDomainEvent
{
    public string Id { get; init; }
}