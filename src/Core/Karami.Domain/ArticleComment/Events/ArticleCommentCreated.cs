﻿using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.ArticleComment.Events;

[MessageBroker(Queue = Broker.AggregateArticle_ArticleComment_Queue)]
public class ArticleCommentCreated : CreateDomainEvent
{
    public string Id        { get; init; }
    public string OwnerId   { get; init; }
    public string ArticleId { get; init; }
    public string Comment   { get; init; }
}