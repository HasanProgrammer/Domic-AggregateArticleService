﻿using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Article.Events;

[EventConfig(Queue = Broker.AggregateArticle_Article_Queue)]
public class ArticleUpdated : UpdateDomainEvent<string>
{
    //Article info ( Aggregte entity | Aggregte root )
    
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