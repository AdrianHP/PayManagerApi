using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PayManager.ApiService.Models;
using PayManager.Business.Contracts.ApplicationService;
using PayManager.Business.Domain;
using PayManager.Business.Enums;
using PayManager.Business.Implementation.DTOs;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PayManager.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentOrdersController (
        IPaymentOrderApplicationService paymentOrderApplicationService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] PaymentOrderDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<PaymentOrder>(model);
            var products = Mapper.Map<List<Product>>(model.Products);
            var order = await paymentOrderApplicationService.CreateOrder(entity,products);

            return Ok(order);
        }

        [HttpPut("pay")]
        public async Task<IActionResult> PayOrder([FromBody] Guid id)
        {
            try
            {
                var currentEntity = paymentOrderApplicationService
                    .Where(po => po.Id == id)
                    .FirstOrDefault();

                if (currentEntity == null)
                {
                    return NotFound(new { message = "Order not found." });
                }
               await paymentOrderApplicationService.PayOrderAsync(currentEntity);

                return Ok(new { message = "Order updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "The order could not be updated.", error = ex.Message });
            }
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] Guid id)
        {
            try
            {
                var currentEntity = paymentOrderApplicationService
                    .Where(po => po.Id == id)
                    .FirstOrDefault();

                if (currentEntity == null)
                {
                    return NotFound(new { message = "Order not found." });
                }
                await paymentOrderApplicationService.CancelOrderAsync(currentEntity);
                return Ok(new { message = "Order deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "The order could not be deleted.", error = ex.Message });
            }
        }
    }
}
