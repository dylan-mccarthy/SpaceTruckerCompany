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

        public SpaceShipRouteService(ILogger<SpaceShipRouteService> logger, IRepository<SpaceShipRoute> spaceShipRouteRepository)
        {
            _logger = logger;
            _spaceShipRouteRepository = spaceShipRouteRepository;
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
