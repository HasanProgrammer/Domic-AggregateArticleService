using Karami.Core.Common.ClassConsts;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.User.Contracts.Interfaces;
using Karami.Domain.User.Entities;
using Karami.Domain.User.Events;

namespace Karami.UseCase.UserUseCase.Events;

public class CreateUserConsumerEventBusHandler : IConsumerEventBusHandler<UserCreated>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public CreateUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) 
        => _userQueryRepository = userQueryRepository;
    
    [WithTransaction]
    [WithCleanCache(Keies = Cache.AggregateArticles)]
    public void Handle(UserCreated @event)
    {
        var targetUser = _userQueryRepository.FindById(@event.Id);

        if (targetUser is null)
        {
            var newUser = new UserQuery {
                Id        = @event.Id        ,
                CreatedBy = @event.CreatedBy ,
                FirstName = @event.FirstName ,
                LastName  = @event.LastName
            };
        
            _userQueryRepository.Add(newUser);
        }
    }
}