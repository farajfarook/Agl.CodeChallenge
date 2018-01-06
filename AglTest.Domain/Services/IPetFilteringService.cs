using System.Collections.Generic;
using System.Threading.Tasks;
using AglTest.Domain.Models;

namespace AglTest.Domain.Services
{
    public interface IPetFilteringService
    {
        /// <summary>
        /// List pets by the given gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        Task<IEnumerable<Pet>> ListPetsByPersonGenderAsync(PersonGender gender);
    }
}