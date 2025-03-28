﻿using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class InActiveUserConsumerEventBusHandler : IConsumerEventBusHandler<UserInActived>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public InActiveUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) 
        => _userQueryRepository = userQueryRepository;

    public void BeforeHandle(UserInActived @event){}

    [TransactionConfig(Type = TransactionType.Query)]
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

    public void AfternHandle(UserInActived @event){}
}