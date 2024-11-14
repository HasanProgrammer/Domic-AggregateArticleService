using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class UpdateUserConsumerEventBusHandler : IConsumerEventBusHandler<UserUpdated>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public UpdateUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) 
        => _userQueryRepository = userQueryRepository;
    
    [TransactionConfig(Type = TransactionType.Query)]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public void Handle(UserUpdated @event)
    {
        var targetUser = _userQueryRepository.FindById(@event.Id);
        
        targetUser.IsActive    = @event.IsActive ? IsActive.Active : IsActive.InActive;
        targetUser.FirstName   = @event.FirstName;
        targetUser.LastName    = @event.LastName;
        targetUser.UpdatedBy   = @event.UpdatedBy;
        targetUser.UpdatedRole = @event.UpdatedRole;
        targetUser.UpdatedAt_EnglishDate = @event.UpdatedAt_EnglishDate;
        targetUser.UpdatedAt_PersianDate = @event.UpdatedAt_PersianDate;
            
        _userQueryRepository.Change(targetUser);
    }

    public void AfterTransactionHandle(UserUpdated @event){}
}