using EventApi.Factories;
using EventApi.Handlers;
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
        private readonly ICacheHandler<IEnumerable<Category>> _cacheHandler;
        private const string _cacheCategoryKey = "eventsByCategoryKey";
        public CategoryService(ICategoryRepository categoryRepository, ICacheHandler<IEnumerable<Category>> cacheHandler)
        {
            _categoryRepository = categoryRepository;
            _cacheHandler = cacheHandler;
        }
        public override async Task <GetAllCategoriesReply> GetAllCategories (Empty request, ServerCallContext context)
        {
            var cachedCategories = _cacheHandler.GetFromCache(_cacheCategoryKey);

            if (cachedCategories != null)
            {
                var cachedReply = new GetAllCategoriesReply();
                cachedReply.Category.AddRange(cachedCategories);
                return cachedReply;
            }

            var entities = await _categoryRepository.GetAllAsync
                (
                   orderByDescending: false,
                   sortBy: x => x.CategoryName,
                   filterBy: null
                );

            var categories = entities.Select(CategoryFactory.ToModel).ToList();
            _cacheHandler.SetCache(_cacheCategoryKey, categories);
            var reply = new GetAllCategoriesReply();
            reply.Category.AddRange(categories);
            return reply;
        }
    }
}