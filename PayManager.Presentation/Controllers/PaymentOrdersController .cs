using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayManager.Business.Contracts.ApplicationService;
using PayManager.Business.Contracts.Service;
using PayManager.Business.Domain;
using PayManager.Business.Implementation.DTOs;
using PayManager.Presentation.Extensions;

namespace PayManager.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentOrdersController(
        ILinqService linqService,
        IPaymentOrderApplicationService paymentOrderApplicationService) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Orders(Guid? id, [FromQuery] TableParamsDTO? tableParams = null)
        {
            if (id != null && id != default)
            {
                var order = await paymentOrderApplicationService
                   .Include(o => o.OrderProducts)
                   .ThenInclude(op => op.Product)
                   .FirstOrDefaultAsync(order => order.Id == id);
                var orderDTO = Mapper.Map<PaymentOrderDTO>(order);

                return Json(new { Data = orderDTO });
            }

            var query = paymentOrderApplicationService
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product);
            var result = await linqService.PaginateAndGetData(query, tableParams.Skip, tableParams.Take)
                .Map<PaymentOrder,PaymentOrderDTO>(Mapper);

            return Json(new { Data = result.Data, Count = result.Count });
        }


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
