using SpaceTruckerCompany.API.Models;

namespace SpaceTruckerCompany.API.Service
{
    public interface ITradeService
    {
        public bool ExecuteTrade(string? username, SpaceStation station, SpaceShipEntry ship, TradeItemEntry tradeItem, int quantity);
    }
    public class TradeService : ITradeService
    {
        public readonly ILogger<TradeService> _logger;
        public readonly IAccountService _accountService;
        public readonly ISpaceShipService _spaceShipService;
        public readonly ISpaceStationService _spaceStationService;
        public readonly ITradeItemService _tradeItemService;

        public TradeService(ILogger<TradeService> logger, IAccountService accountService, ISpaceShipService spaceShipService, ISpaceStationService spaceStationService, ITradeItemService tradeItemService)
        {
            _logger = logger;
            _accountService = accountService;
            _spaceShipService = spaceShipService;
            _spaceStationService = spaceStationService;
            _tradeItemService = tradeItemService;
        }

        public bool ExecuteTrade(string? username, SpaceStation station, SpaceShipEntry ship, TradeItemEntry tradeItem, int quantity)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("Username not provided");
            if (station == null) throw new Exception("Station not provided");
            if (ship == null) throw new Exception("Ship not provided");
            if (tradeItem == null) throw new Exception("Trade Item not provided");
            if (quantity <= 0) throw new Exception("Quantity must be greater than 0");

            //Get playing information
            var player = _accountService.GetAccount(username);
            if (player == null) throw new Exception("Unable to find Player Information");

            //Get station information
            var stationEntry = _spaceStationService.GetStation(station.Id);
            if (stationEntry == null) throw new Exception("Unable to find Station Information");

            //Get ship information
            var shipEntry = _spaceShipService.GetEntry(ship.Id);
            if (shipEntry == null) throw new Exception("Unable to find Ship Information");

            //Confirm ship is at station
            if (shipEntry.Location != stationEntry.Name) throw new Exception("Ship is not at station");

            //Get Trade Item Entry information
            var tradeItemEntry = _tradeItemService.GetTradeItemEntry(tradeItem.Id) ?? throw new Exception("Unable to find Trade Item Information");

            //Check if station has item
            if (!stationEntry.TradeItems.Any(t => t.Item == tradeItem.Item)) throw new Exception("Station does not have item in stock");

            //Check if station has enough items in stock
            if (stationEntry.TradeItems.Where(t => t.Item == tradeItem.Item).FirstOrDefault()?.Quantity < quantity) throw new Exception("Not enough items in stock");

            var totalCost = tradeItemEntry.BuyPrice * quantity;
            if (player.Credits < totalCost) throw new Exception("Not enough credits to complete transaction");

            
            _spaceStationService.AddTradeItemEntry(station, tradeItemEntry);

            _spaceShipService.AddTradeItemToShip(shipEntry, tradeItemEntry);

            player.Credits -= totalCost;
            _accountService.UpdateAccount(player);

            return true;
        }

    }
}
