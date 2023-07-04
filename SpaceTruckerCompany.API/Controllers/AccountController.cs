// API Controller for returning information about a Player Account
// Path: SpaceTruckerCompany.API\Controllers\PlayerController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Service;

namespace SpaceTruckerCompany.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet(Name = "PlayerGetAccount")]
    public Player PlayerGet()
    {
        if(User.Identity == null) throw new Exception("Unable to find User Information");
        var username = User.Identity.Name;
        _logger.LogInformation($"Getting Account Information for {username}");
        return _accountService.GetAccount(username);
    }
    [HttpGet("/Admin", Name = "AdminGetAccount")]
    [Authorize(Roles = "Admin")]
    public Player AdminGet(Player player)
    {
        if(User.Identity == null) throw new Exception("Unable to find User Information");
        var username = User.Identity.Name;
        _logger.LogInformation($"Getting Account Information for {username}");
        return _accountService.GetAccount(player.Id);
    }
    [HttpPost(Name = "CreateAccount")]
    public Player Create()
    {
        if(User.Identity == null) throw new Exception("Unable to find User Information");
        var username = User.Identity.Name;
        _logger.LogInformation($"Creating Account for {username}");
        return _accountService.CreateAccount(username);
    }
    [HttpPut(Name = "PlayerUpdateAccount")]
    public Player Update(Player player)
    {
        if(User.Identity == null) throw new Exception("Unable to find User Information");
        var username = User.Identity.Name;
        //verify that user is the same as the player
        if (username != player.Id) throw new Exception("Unable to update account");
        _logger.LogInformation($"Updating Account for {username}");
        return _accountService.UpdateAccount(player);
    }
    [HttpDelete(Name = "PlayerDeleteAccount")]
    public void PlayerDelete(Player player)
    {
        if(User.Identity == null) throw new Exception("Unable to find User Information");
        var username = User.Identity.Name;
        //verify that user is the same as the player
        if (username != player.Id) throw new Exception("Unable to delete account");
        _logger.LogInformation($"Deleting Account for {username}");
        _accountService.DeleteAccount(player);
    }
    [HttpDelete("Admin", Name = "AdminDeleteAccount")]
    [Authorize(Roles = "Admin")]
    public void AdminDelete(Player player)
    {
        if(User.Identity == null) throw new Exception("Unable to find User Information");
        var username = User.Identity.Name;
        _logger.LogInformation($"Deleting Account for {username}");
        _accountService.DeleteAccount(player);
    }

}