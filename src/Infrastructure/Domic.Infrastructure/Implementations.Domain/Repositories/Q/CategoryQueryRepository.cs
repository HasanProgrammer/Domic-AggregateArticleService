using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Domic.Persistence.Contexts.Q;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class CategoryQueryRepository(SQLContext sqlContext) : ICategoryQueryRepository;

//Transaction
public partial class CategoryQueryRepository
{
    public Task AddAsync(CategoryQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Categories.Add(entity);

        return Task.CompletedTask;
    }

    public Task ChangeAsync(CategoryQuery entity, CancellationToken cancellationToken)
    {
        sqlContext.Categories.Update(entity);

        return Task.CompletedTask;
    }
}

//Query
public partial class CategoryQueryRepository
{
    public Task<CategoryQuery> FindByIdAsync(object id, CancellationToken cancellationToken)
        => sqlContext.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == id as string, cancellationToken);
}