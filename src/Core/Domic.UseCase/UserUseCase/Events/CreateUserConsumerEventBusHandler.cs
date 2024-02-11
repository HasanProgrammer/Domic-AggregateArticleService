using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Entities;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

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
                Id          = @event.Id          ,
                CreatedBy   = @event.CreatedBy   ,
                CreatedRole = @event.CreatedRole ,
                FirstName   = @event.FirstName   ,
                LastName    = @event.LastName    ,
                CreatedAt_EnglishDate = @event.CreatedAt_EnglishDate,
                CreatedAt_PersianDate = @event.CreatedAt_PersianDate
            };
        
            _userQueryRepository.Add(newUser);
        }
    }
}