using System.Collections.Generic;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.SeedWork;

namespace AglTest.Domain.Repositories
{
    public interface IPersonRepository: IRepository<Person>
    {
        Task<IEnumerable<Person>> ListAsync();
    }
}