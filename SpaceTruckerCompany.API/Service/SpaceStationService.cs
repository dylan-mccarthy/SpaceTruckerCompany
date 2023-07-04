using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Data;

namespace SpaceTruckerCompany.API.Service
{
    public interface ISpaceStationService
    {
        public SpaceStation AddStation(SpaceStation station);
        public void RemoveStation(SpaceStation station);
        public SpaceStation UpdateStation(SpaceStation station);
        public SpaceStation GetStation(string? id);
        public List<SpaceStation> GetStations();
        public void AddTradeItemEntry(SpaceStation station, TradeItemEntry entry);
        public void RemoveTradeItemEntry(SpaceStation station, TradeItemEntry entry);

    }
    public class SpaceStationService : ISpaceStationService
    {
        private readonly ILogger<SpaceStationService> _logger;
        private readonly IRepository<SpaceStation> _stationRepository;

        public SpaceStationService(ILogger<SpaceStationService> logger, IRepository<SpaceStation> stationRepository)
        {
            _logger = logger;
            _stationRepository = stationRepository;
        }

        public SpaceStation AddStation(SpaceStation station)
        {
            if (station == null) throw new Exception("Station not provided");
            return _stationRepository.Create(station);
        }
        public void RemoveStation(SpaceStation station)
        {
            if (station == null) throw new Exception("Station not provided");
            _stationRepository.Delete(station);
        }
        public SpaceStation UpdateStation(SpaceStation station)
        {
            if (station == null) throw new Exception("Station not provided");
            return _stationRepository.Update(station);
        }
        public SpaceStation GetStation(string? id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Station Id not provided");
            var station = _stationRepository.Search(s => s.Id == id).FirstOrDefault();
            return station ?? throw new Exception("Unable to find Station Information");
        }
        public List<SpaceStation> GetStations()
        {
            return _stationRepository.Search(s => true).ToList();
        }

        public void AddTradeItemEntry(SpaceStation station, TradeItemEntry entry)
        {
            //Check if station has item
            //If it does, update entry
            //If it doesn't, add entry
            if(station == null) throw new Exception("Station not provided");
            if(entry == null) throw new Exception("Entry not provided");

            if (station.TradeItems.FirstOrDefault(t => t.Item == entry.Item) is { } item)
            {
                  item.Quantity += entry.Quantity;
            }
            else
            {
                station.TradeItems.Add(entry);
            }
            _stationRepository.Update(station);

        }

        public void RemoveTradeItemEntry(SpaceStation station, TradeItemEntry entry)
        {
            if(station == null) throw new Exception("Station not provided");
            if(entry == null) throw new Exception("Entry not provided");

            if (station.TradeItems.FirstOrDefault(t => t.Item == entry.Item) is { } item)
            {
                item.Quantity -= entry.Quantity;

                if (item.Quantity == 0)
                {
                    station.TradeItems.Remove(item);
                }
            }
            _stationRepository.Update(station);

        }
    }
}
