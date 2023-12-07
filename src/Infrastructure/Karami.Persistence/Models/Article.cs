using Karami.Core.Domain.Enumerations;
using MongoDB.Entities;

namespace Karami.Persistence.Models;

public class Article : Entity
{
    public string Id                      { get; set; }
    public string UserId                  { get; set; }
    public string CategoryId              { get; set; }
    public string Title                   { get; set; }
    public string Summary                 { get; set; }
    public string Body                    { get; set; }
    public IsActive IsActive              { get; set; } = IsActive.Active;
    public IsDeleted IsDeleted            { get; set; } = IsDeleted.UnDelete;
    public DateTime CreatedAt_EnglishDate { get; set; }
    public string CreatedAt_PersianDate   { get; set; }
    public DateTime UpdatedAt_EnglishDate { get; set; }
    public string UpdatedAt_PersianDate   { get; set; }
}