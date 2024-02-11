using Domic.Domain.Category.Contracts.Interfaces;
using Domic.Domain.Category.Entities;
using Domic.Persistence.Contexts.Q;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.Q;

//Config
public partial class CategoryQueryRepository : ICategoryQueryRepository
{
    private readonly SQLContext _sqlContext;

    public CategoryQueryRepository(SQLContext sqlContext) => _sqlContext = sqlContext;
}

//Transaction
public partial class CategoryQueryRepository
{
    public void Add(CategoryQuery entity) => _sqlContext.Categories.Add(entity);

    public void Change(CategoryQuery entity) => _sqlContext.Categories.Update(entity);
}

//Query
public partial class CategoryQueryRepository
{
    public CategoryQuery FindById(object id) => 
        _sqlContext.Categories.FirstOrDefault(category => category.Id.Equals(id));
}