using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Service;

namespace SpaceTruckerCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class SpaceShipController : ControllerBase
    {
        private readonly ILogger<SpaceShipController> _logger;
        private readonly ISpaceShipService _spaceShipService;
        private readonly IAccountService _accountService;

        public SpaceShipController(ILogger<SpaceShipController> logger, ISpaceShipService spaceShipService, IAccountService accountService)
        {
            _logger = logger;
            _spaceShipService = spaceShipService;
            _accountService = accountService;
        }

        [HttpGet(Name = "GetSpaceShips")]
        public IEnumerable<SpaceShip> Get()
        {
            _logger.LogInformation("Getting SpaceShip Information");
            return _spaceShipService.GetShips();
        }
        [HttpPost(Name = "AddShip")]
        [Authorize(Roles = "Admin")]
        public SpaceShip Add(SpaceShip ship)
        {
            _logger.LogInformation($"Adding SpaceShip {ship.Id}");
            return _spaceShipService.AddShip(ship);
        }
        [HttpPut(Name = "UpdateShip")]
        [Authorize(Roles = "Admin")]
        public SpaceShip Update(SpaceShip ship)
        {
            _logger.LogInformation($"Updating SpaceShip {ship.Id}");
            return _spaceShipService.UpdateShip(ship);
        }
        [HttpDelete(Name = "DeleteShip")]
        [Authorize(Roles = "Admin")]
        public void Delete(SpaceShip ship)
        {
            _logger.LogInformation($"Deleting SpaceShip {ship.Id}");
            _spaceShipService.RemoveShip(ship);
        }
        [HttpPost(Name = "GetShipsForPlayer")]
        public IEnumerable<SpaceShipEntry> GetShipsForPlayer()
        {
            if(User.Identity == null) throw new Exception("Unable to find User Information");
            var username = User.Identity.Name;
            _logger.LogInformation($"Getting SpaceShips for {username}");
            var player = _accountService.GetAccount(username);
            return _spaceShipService.GetEntriesForPlayer(player);
        }
        [HttpPost(Name = "BuyShip")]
        public SpaceShipEntry BuyShip(SpaceShip ship)
        {
            if(User.Identity == null) throw new Exception("Unable to find User Information");
            var username = User.Identity.Name;
            _logger.LogInformation($"Buying SpaceShip {ship.Id} for {username}");
            var player = _accountService.GetAccount(username);
            return _spaceShipService.BuyShip(player, ship);
        }

    }
}
