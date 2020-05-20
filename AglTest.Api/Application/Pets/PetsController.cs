using System.Threading.Tasks;
using AglTest.Api.Application.Pets.Handlers;
using Enbiso.NLib.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace AglTest.Api.Application.Pets
{
    [ApiController]
    [Route("pets")]
    public class PetsController : ControllerBase
    {
        private readonly ICommandBus _bus;

        public PetsController(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public Task<ListPetsResponse> List([FromQuery] ListPets query) => _bus.Send(query);
        
        [HttpGet("_groupByGender")]
        public Task<ListPetsGroupByGenderResponse> GroupByGender() => _bus.Send(new ListPetsGroupedByGender());
    }
}