using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Domic.Domain.Category.Events;

namespace Domic.UseCase.CategoryUseCase.Events;

public class CreateCategoryConsumerEventBusHandler : IConsumerEventBusHandler<CategoryCreated>
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public CreateCategoryConsumerEventBusHandler(ICategoryQueryRepository categoryQueryRepository) 
        => _categoryQueryRepository = categoryQueryRepository;
    
    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}|{Cache.AggregateArticles}")]
    public void Handle(CategoryCreated @event)
    {
        var targetCategory = _categoryQueryRepository.FindById(@event.Id);

        if (targetCategory is null)
        {
            var newCategory = new CategoryQuery {
                Id          = @event.Id,
                CreatedBy   = @event.CreatedBy,
                CreatedRole = @event.CreatedRole,
                Name        = @event.Name,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate
            };
        
            _categoryQueryRepository.Add(newCategory);
        }
    }
}