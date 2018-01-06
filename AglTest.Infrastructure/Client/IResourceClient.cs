using System.Collections.Generic;
using System.Threading.Tasks;
using AglTest.Domain.Exceptions;

namespace AglTest.Infrastructure.Client
{
    public interface IResourceClient
    {
        Task<string> GetAsync(string request);
    }
}