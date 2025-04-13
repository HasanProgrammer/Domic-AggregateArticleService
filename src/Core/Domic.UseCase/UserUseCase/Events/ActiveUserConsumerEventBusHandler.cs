using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class ActiveUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) : IConsumerEventBusHandler<UserActived>
{
    public Task BeforeHandleAsync(UserActived @event, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Query)]
    public async Task HandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        var targetUser = await userQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

        if (targetUser is not null)
        {
            targetUser.IsActive    = IsActive.Active;
            targetUser.UpdatedBy   = @event.UpdatedBy;
            targetUser.UpdatedRole = @event.UpdatedRole;
            targetUser.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetUser.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            await userQueryRepository.ChangeAsync(targetUser, cancellationToken);
        }
    }

    public Task AfterHandleAsync(UserActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}