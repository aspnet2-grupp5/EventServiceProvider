using EventApi.Models;
using EventApi.Repositories;

namespace EventApi.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetAllStatusesAsync();
        Task<Status?> GetStatusByIdAsync(string id);
        Task<Status?> GetStatusByNameAsync(string statusName);
    }

    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<Status>> GetAllStatusesAsync()
        {
            var entities = await _statusRepository.GetAllAsync();
            return entities.Select(e => new Status
            {
                StatusId = e.StatusId,
                StatusName = e.StatusName
            });
        }

        public async Task<Status?> GetStatusByIdAsync(string id)
        {
            var entity = await _statusRepository.GetByIdAsync(id);
            if (entity == null) return null;

            return new Status
            {
                StatusId = entity.StatusId,
                StatusName = entity.StatusName
            };
        }

        public async Task<Status?> GetStatusByNameAsync(string statusName)
        {
            var entity = await _statusRepository.GetByNameAsync(statusName);
            if (entity == null) return null;

            return new Status
            {
                StatusId = entity.StatusId,
                StatusName = entity.StatusName
            };
        }
    }
}
