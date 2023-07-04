using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Service;

namespace SpaceTruckerCompany.API
{
    public class ServerTick : BackgroundService
    {
        private readonly ILogger<ServerTick> _logger;
        private readonly ISpaceShipService _spaceShipService;
        private readonly ISpaceStationService _spaceStationService;
        private readonly ITradeItemService _tradeItemService;
        private readonly ISpaceShipRouteService _spaceShipRouteService;

        public ServerTick(ILogger<ServerTick> logger, ISpaceShipService spaceShipService, ISpaceStationService spaceStationService, ITradeItemService tradeItemService, ISpaceShipRouteService spaceShipRouteService)
        {
            _logger = logger;
            _spaceShipService = spaceShipService;
            _spaceStationService = spaceStationService;
            _tradeItemService = tradeItemService;
            _spaceShipRouteService = spaceShipRouteService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Server Tick: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                // Server Loop Start

                // Move ships along their routes
                var shipRoutes = _spaceShipRouteService.GetAllSpaceShipRoutes();
                var finishedRoutes = new List<SpaceShipRoute>();
                foreach(var route in shipRoutes)
                {
                    //Get Ship
                    var shipEntry = _spaceShipService.GetEntry(route.ShipId);

                    //Get destination
                    var destinationX = route.DestinationCoordinates[0];
                    var destinationY = route.DestinationCoordinates[1];
                    //Get current location
                    var currentLocationX = route.CurrentCoordinates[0];
                    var currentLocationY = route.CurrentCoordinates[1];

                    //Calculate distance to destination
                    var distanceToDestination = Math.Sqrt(Math.Pow(destinationX - currentLocationX, 2) + Math.Pow(destinationY - currentLocationY, 2));
                    
                    //Calculate movement direction
                    var directionX = (destinationX - currentLocationX) / distanceToDestination;
                    var directionY = (destinationY - currentLocationY) / distanceToDestination;

                    //Get ship speed
                    var speed = shipEntry.Ship.Speed;

                    //Calculate new location
                    var newLocationX = currentLocationX + (directionX * speed);
                    var newLocationY = currentLocationY + (directionY * speed);

                    //Calcuate distance travelled
                    var distanceTravelled = Math.Sqrt(Math.Pow(newLocationX - currentLocationX, 2) + Math.Pow(newLocationY - currentLocationY, 2));

                    //Calcuate fuel used
                    var fuelUsed = distanceTravelled * shipEntry.FuelUsageRate;
                    shipEntry.CurrentFuel -= fuelUsed;

                    //Update ship location
                    route.CurrentCoordinates[0] = (int)newLocationX;
                    route.CurrentCoordinates[1] = (int)newLocationY;

                    //Check if ship has arrived
                    if(distanceToDestination == 0)
                    {
                        shipEntry.Location = _spaceStationService.GetStation(route.StationId).Name;
                        finishedRoutes.Add(route);
                    }
                    _spaceShipService.UpdateEntry(shipEntry);
                }
                //Remove finished routes
                foreach(var route in finishedRoutes)
                {
                    _spaceShipRouteService.DeleteSpaceShipRoute(route.Id);
                }

            }
        }
    }
}