using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO;

namespace OrderProcessing.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderProcessorController : ControllerBase
    {
        private readonly IOrderProcessorBusiness _orderProcessor;
        private readonly ILogger<OrderProcessorController> _logger;

        /// <summary>
        ///  Order processor controller
        /// </summary>
        /// <param name="orderProcessor"></param>
        /// <param name="logger"></param>
        public OrderProcessorController(IOrderProcessorBusiness orderProcessor, ILogger<OrderProcessorController> logger)
        {
            _orderProcessor = orderProcessor;
            _logger = logger;
        }

        [HttpPost]
        [Route("OrderSubmit")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> OrderSubmit(Order order)
        {
            try
            {
                var result = _orderProcessor.SubmitOrder(order);
                return new StatusCodeResult(201);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
