using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Data;

namespace SpaceTruckerCompany.API.Service
{
    public interface ISpaceShipRouteService
    {
        public SpaceShipRoute GetSpaceShipRoute(int id);
        public SpaceShipRoute AddSpaceShipRoute(SpaceShipRoute spaceShipRoute);
        public SpaceShipRoute UpdateSpaceShipRoute(SpaceShipRoute spaceShipRoute);
        public void DeleteSpaceShipRoute(int id);
        public void DeleteSpaceShipRouteBatch(List<SpaceShipRoute> spaceShipRouteBatch);
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

        public SpaceShipRoute GetSpaceShipRoute(int id)
        {
            var spaceShipRoute = _spaceShipRouteRepository.Search(s => s.Id == id).FirstOrDefault();
            return spaceShipRoute ?? throw new Exception("Unable to find SpaceShipRoute Information");
        }

        public SpaceShipRoute AddSpaceShipRoute(SpaceShipRoute spaceShipRoute)
        {
            if (spaceShipRoute == null) throw new Exception("SpaceShipRoute not provided");
            //Validate that the ship has enough fuel to get to destination
            var shipEntry = _spaceShipService.GetEntry(spaceShipRoute.ShipId);
            //Calculate distance of route
            var currentLocationX = spaceShipRoute.CurrentCoordinatesX;
            var currentLocationY = spaceShipRoute.CurrentCoordinatesY;

            var destinationLocationX = spaceShipRoute.DestinationCoordinatesX;
            var destinationLocationY = spaceShipRoute.DestinationCoordinatesY;

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

        public void DeleteSpaceShipRoute(int id)
        {
            var spaceShipRoute = _spaceShipRouteRepository.Search(s => s.Id == id).FirstOrDefault();
            if (spaceShipRoute == null) throw new Exception("Unable to find SpaceShipRoute Information");
            _spaceShipRouteRepository.Delete(spaceShipRoute);
        }

        public void DeleteSpaceShipRouteBatch(List<SpaceShipRoute> spaceShipRouteBatch)
        {
            _spaceShipRouteRepository.DeleteBatch(spaceShipRouteBatch);
        }

        public List<SpaceShipRoute> GetAllSpaceShipRoutes()
        {
            return _spaceShipRouteRepository.Search(s => true).ToList();
        }

    }
}
