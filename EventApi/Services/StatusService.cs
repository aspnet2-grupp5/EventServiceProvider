using EventApi.Factories;
using EventApi.Handlers;
using EventApi.Protos;
using EventApi.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Status = EventApi.Protos.Status;

namespace EventApi.Services
{
    public interface IStatusService
    {
        Task<GetAllStatusesReply> GetAllStatuses (Empty request, ServerCallContext context);
    }

    public class StatusService : StatusProto.StatusProtoBase,IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ICacheHandler<IEnumerable<Status>> _cacheHandler;
        private const string _cacheStatusKey = "statuses_cache_key";


        public StatusService(IStatusRepository statusRepository, ICacheHandler<IEnumerable<Status>> cacheHandler)
        {
            _statusRepository = statusRepository;
            _cacheHandler = cacheHandler;

        }
        public override async Task<GetAllStatusesReply> GetAllStatuses(Empty request, ServerCallContext context)
        {
            var cachedStatuses = _cacheHandler.GetFromCache(_cacheStatusKey);
            if (cachedStatuses != null)
            {
                var cachedReply = new GetAllStatusesReply();
                cachedReply.Status.AddRange(cachedStatuses);
                return cachedReply;
            }

            var entities = await _statusRepository.GetAllAsync
                (
                    orderByDescending: false,
                    sortBy: x => x.StatusName,
                    filterBy: null
                );

            var statuses = entities.Select(StatusFactory.ToModel).ToList();
            _cacheHandler.SetCache(_cacheStatusKey, statuses);
            var reply = new GetAllStatusesReply();
            reply.Status.AddRange(statuses);
            return reply;
        }


        //public async Task<StatusModel?> GetStatusByIdAsync(string id)
        //{
        //    var entity = await _statusRepository.GetByIdAsync(id);
        //    if (entity == null) return null;

        //    return new StatusModel
        //    {
        //        StatusId = entity.StatusId,
        //        StatusName = entity.StatusName
        //    };
        //}

        //public async Task<StatusModel?> GetStatusByNameAsync(string statusName)
        //{
        //    var entity = await _statusRepository.GetByNameAsync(statusName);
        //    if (entity == null) return null;

        //    return new StatusModel
        //    {
        //        StatusId = entity.StatusId,
        //        StatusName = entity.StatusName
        //    };
        //}
    }
}
