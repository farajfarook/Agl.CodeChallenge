using System.Collections.Generic;
using System.Threading.Tasks;
using AglTest.Domain.Models;

namespace AglTest.Domain.Services
{
    public interface IPetFilteringService
    {
        Task<IEnumerable<Pet>> ListPetsByPersonGenderAsync(PersonGender gender);
    }
}