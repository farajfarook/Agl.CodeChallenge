using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AglTest.Domain.Models;

namespace AglTest.Domain.Services
{
    public interface IPetCollectionService
    {
        /// <summary>
        /// Get the sets of sorted pets for each gender 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Tuple<PersonGender, IEnumerable<Pet>>>> ListSortedPetsByGenderAsync();
    }
}