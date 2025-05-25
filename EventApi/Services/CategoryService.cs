using EventApi.Factories;
using EventApi.Protos;
using EventApi.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace EventApi.Services
{
    public interface ICategoryService
    {
        Task<GetAllCategoriesReply> GetAllCategories(Empty request, ServerCallContext context);
    }
    public class CategoryService : CategoryProto.CategoryProtoBase, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task <GetAllCategoriesReply> GetAllCategories (Empty request, ServerCallContext context)
        { 
            var entities = await _categoryRepository.GetAllAsync(
                orderByDescending:false,
                sortBy: x => x.CategoryName,
                filterBy: null

                );

            var categories = entities.Select(CategoryFactory.ToModel).ToList();
            var reply = new GetAllCategoriesReply();
            reply.Category.AddRange(categories);
            return reply;

        }
    }
}
