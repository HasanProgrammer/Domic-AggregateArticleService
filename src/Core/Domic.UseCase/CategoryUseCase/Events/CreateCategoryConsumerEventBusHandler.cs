using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Domic.Domain.Category.Events;

namespace Domic.UseCase.CategoryUseCase.Events;

public class CreateCategoryConsumerEventBusHandler(ICategoryQueryRepository categoryQueryRepository) : IConsumerEventBusHandler<CategoryCreated>
{
    public Task BeforeHandleAsync(CategoryCreated @event, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}|{Cache.AggregateArticles}")]
    public async Task HandleAsync(CategoryCreated @event, CancellationToken cancellationToken)
    {
        var targetCategory = await categoryQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

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
        
            await categoryQueryRepository.AddAsync(newCategory, cancellationToken);
        }
    }

    public Task AfterHandleAsync(CategoryCreated @event, CancellationToken cancellationToken) 
        => Task.CompletedTask;
}