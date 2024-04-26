using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class InActiveUserConsumerEventBusHandler : IConsumerEventBusHandler<UserInActived>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public InActiveUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) 
        => _userQueryRepository = userQueryRepository;

    public void Handle(UserInActived @event)
    {
        var targetUser = _userQueryRepository.FindById(@event.Id);

        if (targetUser is not null)
        {
            targetUser.IsActive    = IsActive.InActive;
            targetUser.UpdatedBy   = @event.UpdatedBy;
            targetUser.UpdatedRole = @event.UpdatedRole;
            targetUser.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
            targetUser.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
            _userQueryRepository.Change(targetUser);
        }
    }
}