using Domic.UseCase.ArticleCommentUseCase.DTOs;

namespace Domic.UseCase.ArticleUseCase.DTOs;

public class ArticleDto
{
    //Article
    
    public required string Id                   { get; init; }
    public required string Title                { get; init; }
    public required string Summary              { get; init; }
    public required string Body                 { get; init; }
    public required bool IsDeleted              { get; init; }
    public required bool IsActive               { get; init; }
    public required string CreatedBy            { get; init; }
    public required string CreatedAt_Persian    { get; init; }
    public required string UpdatedAt_Persian    { get; init; }
    public required DateTime CreatedAt_English  { get; init; }
    public required DateTime? UpdatedAt_English { get; init; }
    
    //User
    
    public required string UserName { get; init; }
    
    //Category
    
    public required string CategoryId   { get; init; }
    public required string CategoryName { get; init; }
    
    //File
    
    public required string IndicatorImage { get; init; }
    
    //Comments
    
    public required List<ArticleCommentDto> Comments { get; set; }
}