using API.Consumption.test.Domain.Command.Advertisement;
using API.Consumption.test.Domain.Queries.Input.Advertisement;
using API.Consumption.test.Domain.Repositories;
using API.Consumption.Test.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Teste.Domain.Handlers;

namespace API.Consumption.Test.Api.Controllers
{
    [Route("api/[controller]")]
    public class AdvertisementController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] IHandler<CreateAdvertisementCommand> service, [FromBody] CreateAdvertisementCommand command)
        {
            var result = await service.HandleAsync(command);

            return await CreateResponseAsync(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromServices] IHandler<EditAdvertisementCommand> service, [FromBody] EditAdvertisementCommand command)
        {
            var result = await service.HandleAsync(command);

            return await CreateResponseAsync(result);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete([FromServices] IHandler<DeleteAdvertisementCommand> service, int id)
        {
            var result = await service.HandleAsync(new DeleteAdvertisementCommand(id));

            return await CreateResponseAsync(result);
        }

        [HttpPost("pagination")]
        public async Task<IActionResult> Pagination([FromServices] IAdvertisementRepository service, [FromBody] AdvertisementQuery query)
        {
            var data = await service.Get(query);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromServices] IAdvertisementRepository service, int id)
        {
            var data = await service.Get(id);

            return Ok(data);
        }

        [HttpGet("nomes")]
        public async Task<IActionResult> GetNames([FromServices] IAdvertisementRepository service)
        {
            var data = await service.GetNamesAsync();

            return Ok(data);
        }
    }
}
