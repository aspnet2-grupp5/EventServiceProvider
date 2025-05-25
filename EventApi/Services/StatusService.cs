using EventApi.Factories;
using EventApi.Protos;
using EventApi.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace EventApi.Services
{
    public interface IStatusService
    {
        Task<GetAllStatusesReply> GetAllStatuses (Empty request, ServerCallContext context);
    }

    public class StatusService : StatusProto.StatusProtoBase,IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public override async Task<GetAllStatusesReply> GetAllStatuses(Empty request, ServerCallContext context)
        {

            var entities = await _statusRepository.GetAllAsync(
            orderByDescending: false,
                sortBy: x => x.StatusName,
                filterBy: null
                
             );

            var statuses = entities.Select(StatusFactory.ToModel).ToList();
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
