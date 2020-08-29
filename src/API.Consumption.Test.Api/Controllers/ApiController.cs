using API.Consumption.test.Domain.Repositories;
using API.Consumption.Test.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Consumption.Test.Api.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : BaseController
    {
        [HttpGet("Brands")]
        public async Task<IActionResult> GetBrands([FromServices] IApiRepository service)
        {
            var data = await service.GetBrands();

            return Ok(data);
        }
        [HttpGet("Model/{brandId}")]
        public async Task<IActionResult> GetModel([FromServices] IApiRepository service, string brandId)
        {
            var data = await service.GetModel(brandId);

            return Ok(data);
        }
        [HttpGet("Vehicles/{page}")]
        public async Task<IActionResult> GetVehicles([FromServices] IApiRepository service, string page)
        {
            var data = await service.GetVehicles(page);

            return Ok(data);
        }
        [HttpGet("Version/{modelId}")]
        public async Task<IActionResult> GetVersion([FromServices] IApiRepository service, string modelId)
        {
            var data = await service.GetVersion(modelId);

            return Ok(data);
        }
    }
}
