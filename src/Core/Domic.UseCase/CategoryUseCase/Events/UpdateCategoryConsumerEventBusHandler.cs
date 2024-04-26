using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Events;

namespace Domic.UseCase.CategoryUseCase.Events;

public class UpdateCategoryConsumerEventBusHandler : IConsumerEventBusHandler<CategoryUpdated>
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public UpdateCategoryConsumerEventBusHandler(ICategoryQueryRepository categoryQueryRepository) 
        => _categoryQueryRepository = categoryQueryRepository;
    
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}|{Cache.AggregateArticles}")]
    public void Handle(CategoryUpdated @event)
    {
        var targetCategory = _categoryQueryRepository.FindById(@event.Id);

        targetCategory.Name        = @event.Name;
        targetCategory.UpdatedBy   = @event.UpdatedBy;
        targetCategory.UpdatedRole = @event.UpdatedRole;
        targetCategory.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
        targetCategory.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

        _categoryQueryRepository.Change(targetCategory);
    }
}