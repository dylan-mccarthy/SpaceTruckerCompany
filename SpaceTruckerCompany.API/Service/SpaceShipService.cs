using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Data;

namespace SpaceTruckerCompany.API.Service
{
    public interface ISpaceShipService
    {
        public SpaceShip AddShip(SpaceShip ship);
        public void RemoveShip(SpaceShip ship);
        public SpaceShip UpdateShip(SpaceShip ship);
        public SpaceShip GetShip(string? id);
        public List<SpaceShip> GetShips();

        public void AddEntry(SpaceShipEntry entry);
        public void RemoveEntry(SpaceShipEntry entry);
        public void UpdateEntry(SpaceShipEntry entry);
        public SpaceShipEntry GetEntry(string? id);
        public List<SpaceShipEntry> GetEntriesForPlayer(Player player);
        public bool AddTradeItemToShip(SpaceShipEntry ship, TradeItemEntry tradeItem);
        public bool RemoveTradeItemFromShip(SpaceShipEntry ship, TradeItemEntry tradeItem);

        public SpaceShipEntry BuyShip(Player player, SpaceShip ship);
    }
    public class SpaceShipService : ISpaceShipService
    {
        private readonly ILogger<SpaceShipService> _logger;
        private readonly IRepository<SpaceShip> _shipRepository;
        private readonly IRepository<SpaceShipEntry> _entryRepository;
        private readonly IAccountService _accountService;

        public SpaceShipService(ILogger<SpaceShipService> logger, IRepository<SpaceShip> shipRepository, IRepository<SpaceShipEntry> entryRepository, IAccountService accountService)
        {
            _logger = logger;
            _shipRepository = shipRepository;
            _entryRepository = entryRepository;
            _accountService = accountService;
        }

        public SpaceShip AddShip(SpaceShip ship)
        {
            if (ship == null) throw new Exception("Ship not provided");
            return _shipRepository.Create(ship);
        }
        public void RemoveShip(SpaceShip ship)
        {
            if (ship == null) throw new Exception("Ship not provided");
            _shipRepository.Delete(ship);
        }
        public SpaceShip UpdateShip(SpaceShip ship)
        {
            if (ship == null) throw new Exception("Ship not provided");
            return _shipRepository.Update(ship);
        }
        public SpaceShip GetShip(string? id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Ship Id not provided");
            var ship = _shipRepository.Search(s => s.Id == id).FirstOrDefault();
            return ship ?? throw new Exception("Unable to find Ship Information");
        }

        public List<SpaceShip> GetShips()
        {
            return _shipRepository.Get();
        }
        public void AddEntry(SpaceShipEntry entry)
        {
            if (entry == null) throw new Exception("Entry not provided");
            _entryRepository.Create(entry);
        }
        public void RemoveEntry(SpaceShipEntry entry)
        {
            if (entry == null) throw new Exception("Entry not provided");
            _entryRepository.Delete(entry);
        }
        public void UpdateEntry(SpaceShipEntry entry)
        {
            if (entry == null) throw new Exception("Entry not provided");
            _entryRepository.Update(entry);
        }
        public SpaceShipEntry GetEntry(string? id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Entry Id not provided");
            var entry = _entryRepository.Search(s => s.Id == id).FirstOrDefault();
            return entry ?? throw new Exception("Unable to find Entry Information");
        }

        public List<SpaceShipEntry> GetEntriesForPlayer(Player player)
        {
              if (player == null) throw new Exception("Player not provided");
            return _entryRepository.Search(s => s.Owner == player).ToList();
        }

        public bool AddTradeItemToShip(SpaceShipEntry ship, TradeItemEntry tradeItem)
        {
            if (ship == null) throw new Exception("Ship not provided");
            if (tradeItem == null) throw new Exception("TradeItem not provided");
            //Check if space in cargo for the item
            var cargoSpaceRequired = tradeItem.Item.Volume * tradeItem.Quantity;
            var cargoSpaceAvailable = ship.CargoSpace - ship.UsedCargoSpace;
            if (cargoSpaceAvailable <= cargoSpaceRequired) throw new Exception("Not enough space in cargo for this item");
            ship.Cargo.Add(tradeItem);
            //Add cargo space required to ship total
            ship.UsedCargoSpace += cargoSpaceRequired;
            _entryRepository.Update(ship);

            return true;
        }
        public bool RemoveTradeItemFromShip(SpaceShipEntry ship, TradeItemEntry tradeItem)
        {
            if (ship == null) throw new Exception("Ship not provided");
            if (tradeItem == null) throw new Exception("TradeItem not provided");
            //Check trade item is in ship cargo
            if (!ship.Cargo.Contains(tradeItem)) throw new Exception("TradeItem not in ship cargo");
            var cargoItem = ship.Cargo.Find(c => c.Id == tradeItem.Id) ?? throw new Exception("TradeItem Null");
            //Check if enough of the item is in cargo
            if (cargoItem.Quantity < tradeItem.Quantity) throw new Exception("Not enough of this item in cargo");
            //Remove item from cargo
            cargoItem.Quantity -= tradeItem.Quantity;
            //Remove cargo space required from ship total
            ship.UsedCargoSpace -= cargoItem.Item.Volume * cargoItem.Quantity;
            //if no more of the item in cargo, remove from cargo list
            if (cargoItem.Quantity == 0) ship.Cargo.Remove(cargoItem);
            _entryRepository.Update(ship);

            return true;
        }

        public SpaceShipEntry BuyShip(Player player, SpaceShip ship)
        {
            if (ship == null) throw new Exception("Ship not provided");
            if (player == null) throw new Exception("Player not provided");
            //Check player has enough money
            if (player.Credits < ship.Price) throw new Exception("Player does not have enough money to buy this ship");
            var shipEntry = new SpaceShipEntry
            {
                Owner = player,
                Ship = ship,
                Cargo = new List<TradeItemEntry>(),
                UsedCargoSpace = 0,
                CargoSpace = ship.MaxCargo,
            };
            //Remove money from player
            player.Credits -= ship.Price;
            
            //Add ship to player
            player.Ships.Add(shipEntry);
            //Add ship to database
            _entryRepository.Create(shipEntry);
            //Update player
            _accountService.UpdateAccount(player);

            return shipEntry;

        }
    }
}
