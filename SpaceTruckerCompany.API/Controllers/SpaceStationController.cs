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
    public class SpaceStationController : ControllerBase
    {
        private readonly ILogger<SpaceStationController> _logger;
        private readonly ISpaceStationService _spaceStationService;

        public SpaceStationController(ILogger<SpaceStationController> logger, ISpaceStationService spaceStationService)
        {
            _logger = logger;
            _spaceStationService = spaceStationService;
        }

        [HttpGet]
        public IEnumerable<SpaceStation> Get()
        {
            _logger.LogInformation("Getting SpaceStation Information");
            return _spaceStationService.GetStations();
        }
        [HttpGet]
        [Route("{id}")]
        public SpaceStation Get(int id)
        {
            _logger.LogInformation($"Getting SpaceStation Information for {id}");
            return _spaceStationService.GetStation(id);
        }
        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public SpaceStation Add(SpaceStation station)
        {
            _logger.LogInformation($"Adding SpaceStation {station.Id}");
            return _spaceStationService.AddStation(station);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public SpaceStation Update(SpaceStation station)
        {
            _logger.LogInformation($"Updating SpaceStation {station.Id}");
            return _spaceStationService.UpdateStation(station);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void Delete(SpaceStation station)
        {
              _logger.LogInformation($"Deleting SpaceStation {station.Id}");
            _spaceStationService.RemoveStation(station);
        }
        

    }
}
