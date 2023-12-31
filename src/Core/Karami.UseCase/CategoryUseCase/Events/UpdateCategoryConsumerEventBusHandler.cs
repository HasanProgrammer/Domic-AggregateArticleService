﻿using Karami.Core.Common.ClassConsts;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Category.Contracts.Interfaces;
using Karami.Domain.Category.Events;

namespace Karami.UseCase.CategoryUseCase.Events;

public class UpdateCategoryConsumerEventBusHandler : IConsumerEventBusHandler<CategoryUpdated>
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public UpdateCategoryConsumerEventBusHandler(ICategoryQueryRepository categoryQueryRepository) 
        => _categoryQueryRepository = categoryQueryRepository;
    
    [WithTransaction]
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}|{Cache.AggregateArticles}")]
    public void Handle(CategoryUpdated @event)
    {
        var targetCategory = _categoryQueryRepository.FindById(@event.Id);

        targetCategory.Name = @event.Name;

        _categoryQueryRepository.Change(targetCategory);
    }
}