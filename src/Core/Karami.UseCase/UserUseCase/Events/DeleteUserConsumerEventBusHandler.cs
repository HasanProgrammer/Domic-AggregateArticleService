using Karami.Core.Common.ClassConsts;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.User.Contracts.Interfaces;
using Karami.Domain.User.Events;

namespace Karami.UseCase.UserUseCase.Events;

public class DeleteUserConsumerEventBusHandler : IConsumerEventBusHandler<UserDeleted>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public DeleteUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) 
        => _userQueryRepository = userQueryRepository;

    [WithMaxRetry(Count = 5)]
    [WithTransaction]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public void Handle(UserDeleted @event)
    {
        
    }
}