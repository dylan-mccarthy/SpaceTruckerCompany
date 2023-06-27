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

        public TradeItemEntry BuyTradeItem(SpaceShipEntry ship, TradeItemEntry tradeItem, int amount);

    }
    public class TradeItemService : ITradeItemService
    {
        private readonly ILogger<TradeItemService> _logger;
        private readonly IRepository<TradeItem> _tradeItemRepository;
        private readonly IRepository<TradeItemEntry> _entryRepository;
        private readonly ISpaceShipService _spaceShipService;

        public TradeItemService(ILogger<TradeItemService> logger, IRepository<TradeItem> tradeItemRepository, IRepository<TradeItemEntry> entryRespository, ISpaceShipService spaceShipService)
        {
            _logger = logger;
            _tradeItemRepository = tradeItemRepository;
            _entryRepository = entryRespository;
            _spaceShipService = spaceShipService;
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

        public TradeItemEntry BuyTradeItem(SpaceShipEntry ship, TradeItemEntry tradeItem, int amount)
        {
            if(ship == null) throw new Exception("Ship not provided");
            if(tradeItem == null) throw new Exception("TradeItem not provided");
            var entry = new TradeItemEntry
            {
                Item = tradeItem.Item,
                Ship = ship,
                Quantity = amount
            };

            //Check ship has enough cargo space
            if (ship.CargoSpace < amount * tradeItem.Item.Volume) throw new Exception("Not enough cargo space to buy this item");

            //Check the ship has enough credits
            if(ship.Credits < tradeItem.BuyPrice * amount) throw new Exception("Not enough credits to buy this item");
            _entryRepository.Create(entry);

            //Remove credits from ship
            ship.Credits -= tradeItem.BuyPrice * amount;
            //Add credits to station - TODO

            //Add to cargo hold
            ship.UsedCargoSpace += amount * tradeItem.Item.Volume;
            ship.Cargo.Add(entry);

            //Update ship
            _spaceShipService.UpdateEntry(ship);

            return entry;
        }
    }
}
