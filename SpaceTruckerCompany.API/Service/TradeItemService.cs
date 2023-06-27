using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Data;

namespace SpaceTruckerCompany.API.Service
{
    public interface ITradeItemService
    {
        public void AddTradeItem(TradeItem tradeItem);
        public void RemoveTradeItem(TradeItem tradeItem);
        public void UpdateTradeItem(TradeItem tradeItem);
        public TradeItem GetTradeItem(string? id);

        public void AddTradeItemEntry(TradeItemEntry entry);
        public void RemoveTradeItemEntry(TradeItemEntry entry);
        public void UpdateTradeItemEntry(TradeItemEntry entry);
        public TradeItemEntry GetTradeItemEntry(string? id);

    }
    public class TradeItemService : ITradeItemService
    {
        private readonly ILogger<TradeItemService> _logger;
        private readonly IRepository<TradeItem> _tradeItemRepository;
        private readonly IRepository<TradeItemEntry> _entryRepository;

        public TradeItemService(ILogger<TradeItemService> logger, IRepository<TradeItem> tradeItemRepository, IRepository<TradeItemEntry> entryRespository)
        {
            _logger = logger;
            _tradeItemRepository = tradeItemRepository;
            _entryRepository = entryRespository;
        }

        public void AddTradeItem(TradeItem tradeItem)
        {
            if (tradeItem == null) throw new Exception("TradeItem not provided");
            _tradeItemRepository.Create(tradeItem);
        }
        public void RemoveTradeItem(TradeItem tradeItem)
        {
            if (tradeItem == null) throw new Exception("TradeItem not provided");
            _tradeItemRepository.Delete(tradeItem);
        }
        public void UpdateTradeItem(TradeItem tradeItem)
        {
            if (tradeItem == null) throw new Exception("TradeItem not provided");
            _tradeItemRepository.Update(tradeItem);
        }
        public TradeItem GetTradeItem(string? id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("TradeItem Id not provided");
            var tradeItem = _tradeItemRepository.Search(s => s.Id == id).FirstOrDefault();
            return tradeItem ?? throw new Exception("Unable to find TradeItem Information");
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
    }
}
