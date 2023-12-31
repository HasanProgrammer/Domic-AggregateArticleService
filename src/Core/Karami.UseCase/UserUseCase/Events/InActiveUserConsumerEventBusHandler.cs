﻿using System.Data;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.User.Contracts.Interfaces;
using Karami.Domain.User.Events;

namespace Karami.UseCase.UserUseCase.Events;

public class InActiveUserConsumerEventBusHandler : IConsumerEventBusHandler<UserInActived>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public InActiveUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) 
        => _userQueryRepository = userQueryRepository;

    [WithTransaction(IsolationLevel = IsolationLevel.ReadUncommitted)]
    public void Handle(UserInActived @event)
    {
        var targetUser = _userQueryRepository.FindById(@event.Id);

        if (targetUser is not null)
        {
            targetUser.IsActive = IsActive.InActive;
            
            _userQueryRepository.Change(targetUser);
        }
    }
}