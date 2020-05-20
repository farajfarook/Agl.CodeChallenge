using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain.People.Models;

namespace AglTest.Domain.People
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<Person>> ListAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Person>> ListByGenderAsync(PersonGender gender, CancellationToken cancellationToken);
    }
}