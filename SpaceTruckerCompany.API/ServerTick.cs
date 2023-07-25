using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Service;
using System.Diagnostics;

namespace SpaceTruckerCompany.API
{
    public class ServerTick : BackgroundService
    {
        private readonly ILogger<ServerTick> _logger;
        private readonly IAccountService _accountService;
        private readonly ISpaceShipService _spaceShipService;
        private readonly ISpaceStationService _spaceStationService;
        private readonly ITradeItemService _tradeItemService;
        private readonly ISpaceShipRouteService _spaceShipRouteService;

        public ServerTick(ILogger<ServerTick> logger,IAccountService accountService, ISpaceShipService spaceShipService, ISpaceStationService spaceStationService, ITradeItemService tradeItemService, ISpaceShipRouteService spaceShipRouteService)
        {
            _logger = logger;
            _accountService = accountService;
            _spaceShipService = spaceShipService;
            _spaceStationService = spaceStationService;
            _tradeItemService = tradeItemService;
            _spaceShipRouteService = spaceShipRouteService;
        }

        public void GenerateLoad(int number)
        {

            var player = _accountService.GetAccount("dylan.mccarthy@azenix.com.au");

            var ships = _spaceShipService.GetEntriesForPlayer(player);

            if(ships.Count < number)
            {
                var numberToCreate = number - ships.Count;
                for(int i = 0; i < numberToCreate; i++)
                {
                    var baseShip = _spaceShipService.GetShip(1);
                    var newShip = new SpaceShipEntry();
                    newShip.Owner = player;
                    newShip.Ship = baseShip;
                    //Multiple fuel for testing purposes
                    newShip.CurrentFuel = baseShip.MaxFuel * 100;
                    newShip.CargoSpace = baseShip.MaxCargo;
                    newShip.UsedCargoSpace = 0;
                    newShip.Location = "Space";

                    _spaceShipService.AddEntry(newShip);
                }
            }
            //Reset Fuel
            foreach(var s in ships)
            {
                s.CurrentFuel = s.Ship.MaxFuel * 100;
                _spaceShipService.UpdateEntry(s);
            }

            for (int i = 1; i < number+1; i++)
            {
                double randomStartX = new Random().NextDouble() * 100;
                double randomStartY = new Random().NextDouble() * 100;
                double randomEndX = new Random().NextDouble() * 100;
                double randomEndY = new Random().NextDouble() * 100;
                var route = new SpaceShipRoute();
                route.ShipId = i;
                route.CurrentCoordinatesX = randomStartX;
                route.CurrentCoordinatesY = randomStartY;
                route.DestinationCoordinatesX = randomEndX;
                route.DestinationCoordinatesY = randomEndY;
                route.PlayerId = player.Id;
                route.StationId = 1;

                _spaceShipRouteService.AddSpaceShipRoute(route);
            }

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GenerateLoad(5000);

            var tickTimeTaken = 0;
            var tickTimeGoal = 1000;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Server Tick: {time}", DateTimeOffset.Now);
                if(tickTimeTaken > tickTimeGoal)
                {
                    _logger.LogWarning("Server Overloaded - Server Tick took " + tickTimeTaken + "ms");
                }
                else
                {
                    await Task.Delay(tickTimeGoal - tickTimeTaken, stoppingToken);
                }
                
                // Server Loop Start
                // Start Timer
                var timer = new Stopwatch();
                timer.Start();

                // Move ships along their routes
                var shipRoutes = _spaceShipRouteService.GetAllSpaceShipRoutes();
                
                var finishedRoutes = new List<SpaceShipRoute>();
                var parallelOptions = new ParallelOptions();
                parallelOptions.MaxDegreeOfParallelism = 24;
                Parallel.ForEach(shipRoutes,parallelOptions, route =>
                {
                    //Get Ship
                    var shipEntry = _spaceShipService.GetEntry(route.ShipId);

                    //Get destination
                    var destinationX = route.DestinationCoordinatesX;
                    var destinationY = route.DestinationCoordinatesY;
                    //Get current location
                    var currentLocationX = route.CurrentCoordinatesX;
                    var currentLocationY = route.CurrentCoordinatesY;

                    //Calculate distance to destination
                    var distanceToDestination = Math.Sqrt(Math.Pow(destinationX - currentLocationX, 2) + Math.Pow(destinationY - currentLocationY, 2));

                    if (distanceToDestination > shipEntry.Ship.Speed)
                    {

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
                        route.CurrentCoordinatesX = newLocationX;
                        route.CurrentCoordinatesY = newLocationY;
                    }

                    //Check if ship has arrived
                    if (distanceToDestination == 0 || distanceToDestination < shipEntry.Ship.Speed)
                    {
                        shipEntry.Location = _spaceStationService.GetStation(route.StationId).Name;
                        finishedRoutes.Add(route);
                    }
                });

                //foreach (var route in shipRoutes)
                //{
                //    //Get Ship
                //    var shipEntry = _spaceShipService.GetEntry(route.ShipId);

                //    //Get destination
                //    var destinationX = route.DestinationCoordinatesX;
                //    var destinationY = route.DestinationCoordinatesY;
                //    //Get current location
                //    var currentLocationX = route.CurrentCoordinatesX;
                //    var currentLocationY = route.CurrentCoordinatesY;

                //    //Calculate distance to destination
                //    var distanceToDestination = Math.Sqrt(Math.Pow(destinationX - currentLocationX, 2) + Math.Pow(destinationY - currentLocationY, 2));

                //    if (distanceToDestination > shipEntry.Ship.Speed)
                //    {

                //        //Calculate movement direction
                //        var directionX = (destinationX - currentLocationX) / distanceToDestination;
                //        var directionY = (destinationY - currentLocationY) / distanceToDestination;

                //        //Get ship speed
                //        var speed = shipEntry.Ship.Speed;

                //        //Calculate new location
                //        var newLocationX = currentLocationX + (directionX * speed);
                //        var newLocationY = currentLocationY + (directionY * speed);

                //        //Calcuate distance travelled
                //        var distanceTravelled = Math.Sqrt(Math.Pow(newLocationX - currentLocationX, 2) + Math.Pow(newLocationY - currentLocationY, 2));

                //        //Calcuate fuel used
                //        var fuelUsed = distanceTravelled * shipEntry.FuelUsageRate;
                //        shipEntry.CurrentFuel -= fuelUsed;

                //        //Update ship location
                //        route.CurrentCoordinatesX = newLocationX;
                //        route.CurrentCoordinatesY = newLocationY;
                //    }

                //    //Check if ship has arrived
                //    if(distanceToDestination == 0 || distanceToDestination < shipEntry.Ship.Speed)
                //    {
                //        shipEntry.Location = _spaceStationService.GetStation(route.StationId).Name;
                //        finishedRoutes.Add(route);
                //    }
                //    _spaceShipService.UpdateEntry(shipEntry);
                //}
                //Remove finished routes
                //foreach(var route in finishedRoutes)
                //{
                //    _spaceShipRouteService.DeleteSpaceShipRoute(route.Id);
                //}

                _spaceShipRouteService.DeleteSpaceShipRouteBatch(finishedRoutes);

                //Stop Timer
                timer.Stop();
                tickTimeTaken = (int)timer.ElapsedMilliseconds;
                _logger.LogInformation($"Server Tick took {timer.ElapsedMilliseconds}ms");
                _logger.LogInformation($"Percentage of time spent on moving ships: {timer.ElapsedMilliseconds / 1000.0 * 100}%");
                // Server Loop End

            }
        }
    }
}