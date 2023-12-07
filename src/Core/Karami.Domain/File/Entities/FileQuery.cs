#pragma warning disable CS0649

using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Domain.Article.Entities;

namespace Karami.Domain.File.Entities;

public class FileQuery : EntityQuery<string>
{
    public string ArticleId { get; set; }
    public string Path      { get; set; }
    public string Name      { get; set; }
    public string Extension { get; set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public ArticleQuery Article { get; set; }
}