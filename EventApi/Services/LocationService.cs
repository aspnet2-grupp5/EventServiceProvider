using EventApi.Factories;
using EventApi.Protos;
using EventApi.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace EventApi.Services
{
    public interface ILocationService
    {
        Task<GetAllLocationsReply> GetAllLocations(Empty request, ServerCallContext context);
    }

    public class LocationService : LocationProto.LocationProtoBase, ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public override async Task <GetAllLocationsReply> GetAllLocations (Empty request, ServerCallContext context)
        {

            var entities = await _locationRepository.GetAllAsync(
            orderByDescending: false,
                sortBy: x => x.Address,
                filterBy: null

             );

            var locations = entities.Select(LocationFactory.ToModel).ToList();
            var reply = new GetAllLocationsReply();
            reply.Location.AddRange(locations);
            return reply;
        }

    }

}
