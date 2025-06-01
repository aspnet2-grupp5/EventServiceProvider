using EventApi.Factories;
using EventApi.Handlers;
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
        private readonly ICacheHandler<IEnumerable<Location>> _cacheHandler;
        private const string _cacheLocationKey = "locations_cache_key";

        public LocationService(ILocationRepository locationRepository, ICacheHandler<IEnumerable<Location>> cacheHandler)
        {
            _locationRepository = locationRepository;
            _cacheHandler = cacheHandler;
        }
        public override async Task <GetAllLocationsReply> GetAllLocations (Empty request, ServerCallContext context)
        {
            var cachedLocations = _cacheHandler.GetFromCache(_cacheLocationKey);
            if (cachedLocations != null)
            {
                var cachedReply = new GetAllLocationsReply();
                cachedReply.Location.AddRange(cachedLocations);
                return cachedReply;
            }

            var entities = await _locationRepository.GetAllAsync
                (
                    orderByDescending: false,
                    sortBy: x => x.Address,
                    filterBy: null
                );

            var locations = entities.Select(LocationFactory.ToModel).ToList();
            _cacheHandler.SetCache(_cacheLocationKey, locations);
            var reply = new GetAllLocationsReply();
            reply.Location.AddRange(locations);
            return reply;
        }

    }

}
