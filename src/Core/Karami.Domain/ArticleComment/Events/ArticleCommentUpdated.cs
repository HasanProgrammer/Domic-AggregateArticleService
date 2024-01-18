﻿using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.ArticleComment.Events;

[MessageBroker(Queue = Broker.AggregateArticle_ArticleComment_Queue)]
public class ArticleCommentUpdated : UpdateDomainEvent<string>
{
    public string Comment { get; init; }
}