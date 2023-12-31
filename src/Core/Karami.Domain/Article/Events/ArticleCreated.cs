﻿using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.Article.Events;

[MessageBroker(Queue = Broker.AggregateArticle_Article_Queue)]
public class ArticleCreated : CreateDomainEvent
{
    //Article info ( Aggregte entity | Aggregte root )
    
    public string Id         { get; init; }
    public string UserId     { get; init; }
    public string CategoryId { get; init; }
    public string Title      { get; init; }
    public string Summary    { get; init; }
    public string Body       { get; init; }
    
    //File info
    
    public string FileId        { get; init; }
    public string FilePath      { get; init; }
    public string FileName      { get; init; }
    public string FileExtension { get; init; }
}