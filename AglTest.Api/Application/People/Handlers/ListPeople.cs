using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain.People;
using AglTest.Domain.People.Models;
using Enbiso.NLib.Cqrs;
using Enbiso.NLib.DependencyInjection;

namespace AglTest.Api.Application.People.Handlers
{
    public class ListPeople: ICommand<ListPeopleResponse>
    {
        
    }

    public class ListPeopleResponse : ICommandResponse
    {
        public IEnumerable<Person> Records { get; set; }
        public long Count { get; set; }
    }

    [TransientService]
    public class ListPeopleHandler : ICommandHandler<ListPeople, ListPeopleResponse>
    {
        private readonly IPeopleRepository _repository;

        public ListPeopleHandler(IPeopleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListPeopleResponse> Handle(ListPeople request, CancellationToken cancellationToken)
        {
            var records = await _repository.ListAsync(cancellationToken);
            var persons = records?.ToArray() ?? new Person[0];
            return new ListPeopleResponse
            {
                Count = persons.Length,
                Records = persons
            };
        }
    }
}