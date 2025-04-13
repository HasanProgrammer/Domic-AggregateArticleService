using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Events;

namespace Domic.UseCase.CategoryUseCase.Events;

public class UpdateCategoryConsumerEventBusHandler(
    ICategoryQueryRepository categoryQueryRepository
) : IConsumerEventBusHandler<CategoryUpdated>
{
    public Task BeforeHandleAsync(CategoryUpdated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = $"{Cache.AggregateArticles}|{Cache.AggregateArticles}")]
    public async Task HandleAsync(CategoryUpdated @event, CancellationToken cancellationToken)
    {
        var targetCategory = await categoryQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        targetCategory.Name        = @event.Name;
        targetCategory.UpdatedBy   = @event.UpdatedBy;
        targetCategory.UpdatedRole = @event.UpdatedRole;
        targetCategory.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
        targetCategory.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;

        await categoryQueryRepository.ChangeAsync(targetCategory, cancellationToken);
    }

    public Task AfterHandleAsync(CategoryUpdated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}