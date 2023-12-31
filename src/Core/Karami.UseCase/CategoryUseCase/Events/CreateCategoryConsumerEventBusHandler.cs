﻿using Karami.Core.Common.ClassConsts;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Category.Contracts.Interfaces;
using Karami.Domain.Category.Entities;
using Karami.Domain.Category.Events;

namespace Karami.UseCase.CategoryUseCase.Events;

public class CreateCategoryConsumerEventBusHandler : IConsumerEventBusHandler<CategoryCreated>
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    public CreateCategoryConsumerEventBusHandler(ICategoryQueryRepository categoryQueryRepository) 
        => _categoryQueryRepository = categoryQueryRepository;
    
    [WithTransaction]
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}|{Cache.AggregateArticles}")]
    public void Handle(CategoryCreated @event)
    {
        var targetCategory = _categoryQueryRepository.FindById(@event.Id);

        if (targetCategory is null)
        {
            var newCategory = new CategoryQuery {
                Id   = @event.Id , 
                Name = @event.Name
            };
        
            _categoryQueryRepository.Add(newCategory);
        }
    }
}