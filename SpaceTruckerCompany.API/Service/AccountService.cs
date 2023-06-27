using SpaceTruckerCompany.API.Data;
using SpaceTruckerCompany.API.Models;

namespace SpaceTruckerCompany.API.Service
{
    public interface IAccountService
    {
        public Player GetAccount(string? username);
        public Player CreateAccount(string? username);
        public Player UpdateAccount(Player player);
        public void DeleteAccount(Player player);
        public void AddShipToAccount(Player player, SpaceShipEntry ship);
        public void RemoveShipFromAccount(Player player, SpaceShipEntry ship);

    }
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IRepository<Player> _playerRepository;
        public AccountService(ILogger<AccountService> logger, IRepository<Player> playerRepository) {
            _logger = logger;
            _playerRepository = playerRepository;
        }

        public Player GetAccount(string? username)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("Username not provided");
            var player = _playerRepository.Search(p => p.Id == username).FirstOrDefault();
            return player ?? throw new Exception("Unable to find Player Information");
        }
        public Player CreateAccount(string? username)
        {
            if (string.IsNullOrEmpty(username)) throw new Exception("Username not provided");
            var player = new Player(username);
            _playerRepository.Create(player);
            return player;
        }
        public Player UpdateAccount(Player player)
        {
            if (player == null) throw new Exception("Player not provided");
            _playerRepository.Update(player);
            return player;
        }
        public void DeleteAccount(Player player)
        {
            if (player == null) throw new Exception("Player not provided");
            _playerRepository.Delete(player);
        }
        public void AddShipToAccount(Player player, SpaceShipEntry ship)
        {
            if (player == null) throw new Exception("Player not provided");
            if (ship == null) throw new Exception("Ship not provided");
            player.Ships.Add(ship);
            _playerRepository.Update(player);
        }
        public void RemoveShipFromAccount(Player player, SpaceShipEntry ship)
        {
            if (player == null) throw new Exception("Player not provided");
            if (ship == null) throw new Exception("Ship not provided");
            player.Ships.Remove(ship);
            _playerRepository.Update(player);
        }
    }
}
