using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Data;

namespace SpaceTruckerCompany.API.Service
{
    public interface ISpaceShipRouteService
    {
        public SpaceShipRoute GetSpaceShipRoute(string id);
        public SpaceShipRoute AddSpaceShipRoute(SpaceShipRoute spaceShipRoute);
        public SpaceShipRoute UpdateSpaceShipRoute(SpaceShipRoute spaceShipRoute);
        public void DeleteSpaceShipRoute(string id);
        public List<SpaceShipRoute> GetAllSpaceShipRoutes();

    }
    public class SpaceShipRouteService : ISpaceShipRouteService
    {
        private readonly ILogger<SpaceShipRouteService> _logger;
        private readonly IRepository<SpaceShipRoute> _spaceShipRouteRepository;
        private readonly ISpaceShipService _spaceShipService;

        public SpaceShipRouteService(ILogger<SpaceShipRouteService> logger, IRepository<SpaceShipRoute> spaceShipRouteRepository, ISpaceShipService spaceShipService)
        {
            _logger = logger;
            _spaceShipRouteRepository = spaceShipRouteRepository;
            _spaceShipService = spaceShipService;
        }

        public SpaceShipRoute GetSpaceShipRoute(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("SpaceShipRoute Id not provided");
            var spaceShipRoute = _spaceShipRouteRepository.Search(s => s.Id == id).FirstOrDefault();
            return spaceShipRoute ?? throw new Exception("Unable to find SpaceShipRoute Information");
        }

        public SpaceShipRoute AddSpaceShipRoute(SpaceShipRoute spaceShipRoute)
        {
            if (spaceShipRoute == null) throw new Exception("SpaceShipRoute not provided");
            //Validate that the ship has enough fuel to get to destination
            var shipEntry = _spaceShipService.GetEntry(spaceShipRoute.ShipId);
            //Calculate distance of route
            var currentLocationX = spaceShipRoute.CurrentCoordinates[0];
            var currentLocationY = spaceShipRoute.CurrentCoordinates[1];

            var destinationLocationX = spaceShipRoute.DestinationCoordinates[0];
            var destinationLocationY = spaceShipRoute.DestinationCoordinates[1];

            var distance = Math.Sqrt(Math.Pow(destinationLocationX - currentLocationX, 2) + Math.Pow(destinationLocationY - currentLocationY, 2));
            if(distance == 0) throw new Exception("Destination is the same as current location");
            var fuelRequired = distance * shipEntry.FuelUsageRate;
            if (shipEntry.CurrentFuel < fuelRequired) throw new Exception("Not enough fuel");


            return _spaceShipRouteRepository.Create(spaceShipRoute);
        }

        public SpaceShipRoute UpdateSpaceShipRoute(SpaceShipRoute spaceShipRoute)
        {
            if (spaceShipRoute == null) throw new Exception("SpaceShipRoute not provided");
            return _spaceShipRouteRepository.Update(spaceShipRoute);
        }

        public void DeleteSpaceShipRoute(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("SpaceShipRoute Id not provided");
            var spaceShipRoute = _spaceShipRouteRepository.Search(s => s.Id == id).FirstOrDefault();
            if (spaceShipRoute == null) throw new Exception("Unable to find SpaceShipRoute Information");
            _spaceShipRouteRepository.Delete(spaceShipRoute);
        }

        public List<SpaceShipRoute> GetAllSpaceShipRoutes()
        {
            return _spaceShipRouteRepository.Search(s => true).ToList();
        }

    }
}
