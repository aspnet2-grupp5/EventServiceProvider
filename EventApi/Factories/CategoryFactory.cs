using EventApi.Data.Entities;
using EventApi.Protos;

namespace EventApi.Factories
{
    public class CategoryFactory
    {
        public static Category ToModel (CategoryEntity categoryEntity)
        {
            if (categoryEntity == null) return null!;
            return new Category
            {
                CategoryId = categoryEntity.CategoryId,
                CategoryName = categoryEntity.CategoryName,
            };
        }
    }
}
