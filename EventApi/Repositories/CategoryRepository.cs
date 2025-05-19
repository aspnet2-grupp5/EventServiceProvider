using EventApi.Data.Contexts;
using EventApi.Data.Entities;

namespace EventApi.Repositories
{
    internal interface ICategoryRepository : IBaseRepository<CategoryEntity>
    {
    }
    public class CategoryRepository(EventsDbContext context) : BaseRepository<CategoryEntity>(context), ICategoryRepository
    {
    }

}
