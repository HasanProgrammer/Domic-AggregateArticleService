﻿using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Article.Events;

[EventConfig(Queue = Broker.AggregateArticle_Article_Queue)]
public class ArticleDeleted : UpdateDomainEvent<string>
{
    
}