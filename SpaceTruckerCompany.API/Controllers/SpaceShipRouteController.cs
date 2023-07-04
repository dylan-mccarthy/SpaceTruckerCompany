using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Service;

namespace SpaceTruckerCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class SpaceShipRouteController : ControllerBase
    {
        private readonly ILogger<SpaceShipRouteController> _logger;
        private readonly ISpaceShipRouteService _spaceShipRouteService;
        private readonly IAccountService _accountService;

        public SpaceShipRouteController(ILogger<SpaceShipRouteController> logger, ISpaceShipRouteService spaceShipRouteService, IAccountService accountService)
        {
            _logger = logger;
            _spaceShipRouteService = spaceShipRouteService;
            _accountService = accountService;
        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<SpaceShipRoute> Get()
        {
            _logger.LogInformation("Getting SpaceShipRoute Information");
            return _spaceShipRouteService.GetAllSpaceShipRoutes();
        }
        [HttpGet]
        public SpaceShipRoute Get(string id)
        {
            _logger.LogInformation($"Getting SpaceShipRoute Information for {id}");
            return _spaceShipRouteService.GetSpaceShipRoute(id);
        }
        [HttpPost("Add")]
        public SpaceShipRoute Add(SpaceShipRoute spaceShipRoute)
        {
            _logger.LogInformation($"Adding SpaceShipRoute {spaceShipRoute.Id}");
            //Check that the player is the owner of the ship
            var username = User.Identity?.Name;
            var player = _accountService.GetAccount(username);
            if (player == null) throw new Exception("Unable to find Player Information");
            if (player.Id != spaceShipRoute.PlayerId) throw new Exception("Unable to add SpaceShipRoute for another Player");

            return _spaceShipRouteService.AddSpaceShipRoute(spaceShipRoute);
        }
        [HttpPost("Admin/Add")]
        [Authorize(Roles = "Admin")]
        public SpaceShipRoute AdminAdd(SpaceShipRoute spaceShipRoute)
        {
            _logger.LogInformation($"Adding SpaceShipRoute {spaceShipRoute.Id}");
            return _spaceShipRouteService.AddSpaceShipRoute(spaceShipRoute);
        }

        [HttpPut("Update")]
        public SpaceShipRoute Update(SpaceShipRoute spaceShipRoute)
        {
            _logger.LogInformation($"Updating SpaceShipRoute {spaceShipRoute.Id}");
            //Check that the player is the owner of the ship
            var username = User.Identity?.Name;
            var player = _accountService.GetAccount(username);
            if (player == null) throw new Exception("Unable to find Player Information");
            if (player.Id != spaceShipRoute.PlayerId) throw new Exception("Unable to update SpaceShipRoute for another Player");

            return _spaceShipRouteService.UpdateSpaceShipRoute(spaceShipRoute);
        }

        [HttpPut("Admin/Update")]
        [Authorize(Roles = "Admin")]
        public SpaceShipRoute AdminUpdate(SpaceShipRoute spaceShipRoute)
        {
            _logger.LogInformation($"Updating SpaceShipRoute {spaceShipRoute.Id}");
            return _spaceShipRouteService.UpdateSpaceShipRoute(spaceShipRoute);
        }

        [HttpDelete("Delete")]
        public void Delete(string id)
        {
            _logger.LogInformation($"Deleting SpaceShipRoute {id}");
            //Check that the player is the owner of the ship
            var username = User.Identity?.Name;
            var player = _accountService.GetAccount(username);
            if (player == null) throw new Exception("Unable to find Player Information");
            var spaceShipRoute = _spaceShipRouteService.GetSpaceShipRoute(id);
            if (spaceShipRoute == null) throw new Exception("Unable to find SpaceShipRoute Information");
            if (player.Id != spaceShipRoute.PlayerId) throw new Exception("Unable to delete SpaceShipRoute for another Player");

            _spaceShipRouteService.DeleteSpaceShipRoute(spaceShipRoute.Id);
        }

        [HttpDelete("Admin/Delete")]
        [Authorize(Roles = "Admin")]    
        public void AdminDelete(string id)
        {
            _logger.LogInformation($"Deleting SpaceShipRoute {id}");
            _spaceShipRouteService.DeleteSpaceShipRoute(id);
        }
    }
}
