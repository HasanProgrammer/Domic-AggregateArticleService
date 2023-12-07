using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.User.Contracts.Interfaces;
using Karami.Domain.User.Events;

namespace Karami.UseCase.UserUseCase.Events;

public class UpdateUserConsumerEventBusHandler : IConsumerEventBusHandler<UserUpdated>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public UpdateUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) 
        => _userQueryRepository = userQueryRepository;

    [WithMaxRetry(Count = 5)]
    [WithTransaction]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public void Handle(UserUpdated @event)
    {
        var targetUser = _userQueryRepository.FindById(@event.Id);
        
        targetUser.FirstName = @event.FirstName;
        targetUser.LastName  = @event.LastName;
        targetUser.IsActive  = @event.IsActive ? IsActive.Active : IsActive.InActive;
            
        _userQueryRepository.Change(targetUser);
    }
}