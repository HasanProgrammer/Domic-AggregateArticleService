using Domic.Core.Common.ClassConsts;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.User.Contracts.Interfaces;
using Domic.Domain.User.Entities;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class CreateUserConsumerEventBusHandler(IUserQueryRepository userQueryRepository) : IConsumerEventBusHandler<UserCreated>
{
    public Task BeforeHandleAsync(UserCreated @event, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [WithCleanCache(Keies = Cache.AggregateArticles)]
    [TransactionConfig(Type = TransactionType.Query)]
    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken)
    {
        var targetUser = await userQueryRepository.FindByIdAsync(@event.Id, cancellationToken);

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
        
            await userQueryRepository.AddAsync(newUser, cancellationToken);
        }
    }

    public Task AfterHandleAsync(UserCreated @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}