using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Data;

namespace SpaceTruckerCompany.API.Service
{
    public interface ITradeItemService
    {
        public TradeItem AddTradeItem(TradeItem tradeItem);
        public void RemoveTradeItem(TradeItem tradeItem);
        public TradeItem UpdateTradeItem(TradeItem tradeItem);
        public TradeItem GetTradeItem(string? id);
        public List<TradeItem> GetTradeItems();

        public void AddTradeItemEntry(TradeItemEntry entry);
        public void RemoveTradeItemEntry(TradeItemEntry entry);
        public void UpdateTradeItemEntry(TradeItemEntry entry);
        public TradeItemEntry GetTradeItemEntry(string? id);

        public TradeItemEntry BuyTradeItem(SpaceStation station, SpaceShipEntry ship, TradeItemEntry tradeItem);
        public TradeItemEntry SellTradeItem(SpaceStation station, SpaceShipEntry ship, TradeItemEntry tradeItem);

    }
    public class TradeItemService : ITradeItemService
    {
        private readonly ILogger<TradeItemService> _logger;
        private readonly IRepository<TradeItem> _tradeItemRepository;
        private readonly IRepository<TradeItemEntry> _entryRepository;
        private readonly ISpaceShipService _spaceShipService;
        private readonly ISpaceStationService _spaceStationService;

        public TradeItemService(ILogger<TradeItemService> logger, IRepository<TradeItem> tradeItemRepository, IRepository<TradeItemEntry> entryRespository, ISpaceShipService spaceShipService, ISpaceStationService spaceStationService)
        {
            _logger = logger;
            _tradeItemRepository = tradeItemRepository;
            _entryRepository = entryRespository;
            _spaceShipService = spaceShipService;
            _spaceStationService = spaceStationService;
        }

        public TradeItem AddTradeItem(TradeItem tradeItem)
        {
            if (tradeItem == null) throw new Exception("TradeItem not provided");
            return _tradeItemRepository.Create(tradeItem);
        }
        public void RemoveTradeItem(TradeItem tradeItem)
        {
            if (tradeItem == null) throw new Exception("TradeItem not provided");
            _tradeItemRepository.Delete(tradeItem);
        }
        public TradeItem UpdateTradeItem(TradeItem tradeItem)
        {
            if (tradeItem == null) throw new Exception("TradeItem not provided");
            return _tradeItemRepository.Update(tradeItem);
        }
        public TradeItem GetTradeItem(string? id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("TradeItem Id not provided");
            var tradeItem = _tradeItemRepository.Search(s => s.Id == id).FirstOrDefault();
            return tradeItem ?? throw new Exception("Unable to find TradeItem Information");
        }

        public List<TradeItem> GetTradeItems()
        {
            return _tradeItemRepository.Search(s => true).ToList();
        }

        public void AddTradeItemEntry(TradeItemEntry entry)
        {
            if(entry == null) throw new Exception("Entry not provided");
            _entryRepository.Create(entry);
        }

        public void RemoveTradeItemEntry(TradeItemEntry entry)
        {
            if(entry == null) throw new Exception("Entry not provided");
            _entryRepository.Delete(entry);
        }

        public void UpdateTradeItemEntry(TradeItemEntry entry)
        {
            if(entry == null) throw new Exception("Entry not provided");
            _entryRepository.Update(entry);
        }

        public TradeItemEntry GetTradeItemEntry(string? id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Entry Id not provided");
            var entry = _entryRepository.Search(s => s.Id == id).FirstOrDefault();
            return entry ?? throw new Exception("Unable to find Entry Information");
        }

        public TradeItemEntry BuyTradeItem(SpaceStation station, SpaceShipEntry ship, TradeItemEntry tradeItemEntry)
        {
            if(station == null) throw new Exception("Station not provided");
            if(ship == null) throw new Exception("Ship not provided");
            if(tradeItemEntry == null) throw new Exception("TradeItem not provided");

            _entryRepository.Create(tradeItemEntry);

            //Check ship has enough cargo space
            if (ship.CargoSpace < tradeItemEntry.Quantity * tradeItemEntry.Item.Volume) throw new Exception("Not enough cargo space to buy this item");

            //Remove item from Space Station
            _spaceStationService.RemoveTradeItemEntry(station, tradeItemEntry);

            //Add to cargo hold
            ship.UsedCargoSpace += tradeItemEntry.Quantity * tradeItemEntry.Item.Volume;
            ship.Cargo.Add(tradeItemEntry);

            //Update ship
            _spaceShipService.UpdateEntry(ship);

            _entryRepository.Create(tradeItemEntry);
            return tradeItemEntry;
        }

        public TradeItemEntry SellTradeItem(SpaceStation station, SpaceShipEntry ship, TradeItemEntry tradeItemEntry)
        {
            if (station == null) throw new Exception("Station not provided");
            if (ship == null) throw new Exception("Ship not provided");
            if (tradeItemEntry == null) throw new Exception("TradeItem not provided");

            _entryRepository.Create(tradeItemEntry);

            //Remove item from Space Station
            _spaceStationService.AddTradeItemEntry(station, tradeItemEntry);

            //Remove from cargo hold
            ship.UsedCargoSpace -= tradeItemEntry.Quantity * tradeItemEntry.Item.Volume;

            if (ship.Cargo.FirstOrDefault(t => t.Item == tradeItemEntry.Item) is { } item)
            {
                item.Quantity -= tradeItemEntry.Quantity;

                if (item.Quantity == 0)
                {
                    ship.Cargo.Remove(item);
                }
            }

            //Update ship
            _spaceShipService.UpdateEntry(ship);

            return tradeItemEntry;
        }
    }
}
