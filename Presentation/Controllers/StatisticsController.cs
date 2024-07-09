﻿using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> GetProductRevenues([FromBody]ProductRevenueFilterModel model,[FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _statisticService.GetProductRevenues(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Route("orders")]
        public async Task<IActionResult> GetOrderSummary([FromQuery] OrderSummaryFilterModel model)
        {
            try
            {
                return await _statisticService.GetOrderSummary(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
