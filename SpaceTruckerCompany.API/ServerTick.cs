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

        public ServerTick(ILogger<ServerTick> logger, ISpaceShipService spaceShipService, ISpaceStationService spaceStationService, ITradeItemService tradeItemService)
        {
            _logger = logger;
            _spaceShipService = spaceShipService;
            _spaceStationService = spaceStationService;
            _tradeItemService = tradeItemService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Server Tick: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                //Do Things
            }
        }
    }
}