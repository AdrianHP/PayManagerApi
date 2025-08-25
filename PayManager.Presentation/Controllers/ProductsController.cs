using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayManager.ApiService.Models;
using PayManager.Business.Contracts.ApplicationService;
using PayManager.Business.Contracts.Service;
using PayManager.Business.Domain;
using PayManager.Business.Enums;
using PayManager.Business.Implementation.DTOs;
using System.Buffers;
using System.Collections.Generic;
using System.Xml.Linq;
using PayManager.Presentation.Extensions;
using Newtonsoft.Json;

namespace PayManager.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(
        ILinqService linqService,
        IProductApplicationService productApplicationService) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Product(Guid? id, [FromQuery] TableParamsDTO? tableParams = null)
        {
            if (id != null && id != default)
            {
                var product = await productApplicationService
                   .FirstOrDefaultAsync(product => product.Id == id);
                var productDTO = Mapper.Map<Product>(product);

                return Json(new { Data = productDTO });
            }

            var query = productApplicationService;
              
            var result = await linqService.PaginateAndGetData(query, tableParams.Skip, tableParams.Take)
                .Map<Product,ProductDTO>(Mapper);

            return Json(new { Data = result.Data, Count = result.Count });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDTO model)
        {
            if (!TryValidateModel(model))
                return BadRequest(ModelState);

            var entity = Mapper.Map<Product>(model);
            await productApplicationService.AddAsync(entity);
            model.Id = entity.Id;
            return Ok(model);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, ProductDTO model)
        {
            var entity = Mapper.Map<Product>(model);
            entity.Id = id;
            var currentEntity = productApplicationService.Where(p=>p.Id == id).FirstOrDefault();
            if (currentEntity == null)
            {
                return NotFound("The entity does not exist");
            }
            await productApplicationService.UpdateAsync(entity);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var currentEntity = productApplicationService.Where(cn => cn.Id == id).FirstOrDefault();
            if (currentEntity == null)
            {
                return NotFound("The entity does not exist");
            }
            await productApplicationService.DeleteAsync(currentEntity);
            return Ok();
        }




    }
}
