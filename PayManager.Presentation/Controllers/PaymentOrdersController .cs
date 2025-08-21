using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<PaymentOrderDTO>> CreateOrder([FromBody] PaymentOrderDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map<PaymentOrder>(model);
            var products = Mapper.Map<List<Product>>(model.Products);
            var order = await paymentOrderApplicationService.CreateOrder(entity,products);
            return CreatedAtAction(nameof(PaymentOrder), new { orderId = order.OrderId }, order);
        }
    }
}
