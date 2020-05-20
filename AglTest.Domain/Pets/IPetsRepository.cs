using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain.Models;

namespace AglTest.Domain.Pets
{
    public interface IPetsRepository
    {
        Task<IEnumerable<Pet>> ListAsync(string ownerName, CancellationToken cancellationToken = default);
    }
}