// API Controller for returning information about a Player Account
// Path: SpaceTruckerCompany.API\Controllers\PlayerController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpaceTruckerCompany.API.Data;
namespace SpaceTruckerCompany.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly ApplicationDbContext _context;

    public AccountController(ILogger<AccountController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetAccount")]
    public Player Get()
    {
        var userId = User.Identity.Name;
        var player = _context.Players.FirstOrDefault(p => p.Id == userId);
        return player;
    }
}