﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpaceTruckerCompany.API.Models;
using SpaceTruckerCompany.API.Service;

namespace SpaceTruckerCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class TradeItemsController : ControllerBase
    {
        private readonly ILogger<TradeItemsController> _logger;
        private readonly ITradeItemService _tradeItemService;

        public TradeItemsController(ILogger<TradeItemsController> logger, ITradeItemService tradeItemService)
        {
            _logger = logger;
            _tradeItemService = tradeItemService;
        }

        [HttpGet(Name = "GetTradeItems")]
        public IEnumerable<TradeItem> Get()
        {
            _logger.LogInformation("Getting TradeItem Information");
            return _tradeItemService.GetTradeItems();
        }
        [HttpPost("Add", Name = "AddTradeItem")]
        [Authorize(Roles = "Admin")]
        public TradeItem Add(TradeItem tradeItem)
        {
            _logger.LogInformation($"Adding TradeItem {tradeItem.Id}");
            return _tradeItemService.AddTradeItem(tradeItem);
        }
        [HttpPut(Name = "UpdateTradeItem")]
        [Authorize(Roles = "Admin")]
        public TradeItem Update(TradeItem tradeItem)
        {
            _logger.LogInformation($"Updating TradeItem {tradeItem.Id}");
            return _tradeItemService.UpdateTradeItem(tradeItem);
        }
        [HttpDelete(Name = "DeleteTradeItem")]
        [Authorize(Roles = "Admin")]
        public void Delete(TradeItem tradeItem)
        {
              _logger.LogInformation($"Deleting TradeItem {tradeItem.Id}");
            _tradeItemService.RemoveTradeItem(tradeItem);
        }
        [HttpPost("Buy", Name = "BuyTradeItem")]
        public TradeItemEntry BuyTradeItem(TradeRequest request)
        {
            _logger.LogInformation($"Recieved Request with ID: {request.Id}");
            return _tradeItemService.BuyTradeItem(request.Station, request.Ship, request.Item);
        }
        [HttpPost("Sell", Name = "SellTradeItem")]
        public TradeItemEntry SellTradeItem(TradeRequest request)
        {
            _logger.LogInformation($"Recieved Request with ID: {request.Id}");
            return _tradeItemService.SellTradeItem(request.Station, request.Ship, request.Item);
        }
    }
}
