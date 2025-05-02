using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class InActiveUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) : IConsumerEventBusHandler<UserInActived>
{
    public Task BeforeHandleAsync(UserInActived @event, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public async Task HandleAsync(UserInActived @event, CancellationToken cancellationToken)
    {
        var targetUser = await userQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        if (targetUser is not null)
        {
            targetUser.IsActive    = IsActive.InActive;
            targetUser.UpdatedBy   = @event.UpdatedBy;
            targetUser.UpdatedRole = @event.UpdatedRole;
            targetUser.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetUser.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            await userQueryRepository.ChangeAsync(targetUser, cancellationToken);
        }
    }

    public Task AfterHandleAsync(UserInActived @event, CancellationToken cancellationToken) 
        => Task.CompletedTask;
}