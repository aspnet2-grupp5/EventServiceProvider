//using EventApi.Models;
//using EventApi.Repositories;

//namespace EventApi.Services
//{
//    public interface IStatusService
//    {
//        Task<IEnumerable<StatusModel>> GetAllStatusesAsync();
//        Task<StatusModel?> GetStatusByIdAsync(string id);
//        Task<StatusModel?> GetStatusByNameAsync(string statusName);
//    }

//    public class StatusService : IStatusService
//    {
//        private readonly IStatusRepository _statusRepository;

//        public StatusService(IStatusRepository statusRepository)
//        {
//            _statusRepository = statusRepository;
//        }

//        public async Task<IEnumerable<StatusModel>> GetAllStatusesAsync()
//        {
//            var entities = await _statusRepository.GetAllAsync();
//            return entities.Select(e => new StatusModel
//            {
//                StatusId = e.StatusId,
//                StatusName = e.StatusName
//            });
//        }

//        public async Task<StatusModel?> GetStatusByIdAsync(string id)
//        {
//            var entity = await _statusRepository.GetByIdAsync(id);
//            if (entity == null) return null;

//            return new StatusModel
//            {
//                StatusId = entity.StatusId,
//                StatusName = entity.StatusName
//            };
//        }

//        public async Task<StatusModel?> GetStatusByNameAsync(string statusName)
//        {
//            var entity = await _statusRepository.GetByNameAsync(statusName);
//            if (entity == null) return null;

//            return new StatusModel
//            {
//                StatusId = entity.StatusId,
//                StatusName = entity.StatusName
//            };
//        }
//    }
//}
