using System.Threading.Tasks;
using AglTest.Api.Application.People.Handlers;
using Enbiso.NLib.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace AglTest.Api.Application.People
{
    [ApiController]
    [Route("people")]
    public class PeopleController: ControllerBase
    {
        private readonly ICommandBus _bus;

        public PeopleController(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public Task<ListPeopleResponse> List() => _bus.Send(new ListPeople());
    }
}