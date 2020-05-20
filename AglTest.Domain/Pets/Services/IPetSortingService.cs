using System.Collections.Generic;
using AglTest.Domain.Models;

namespace AglTest.Domain.Pets.Services
{
    public interface IPetSortingService
    {
        /// <summary>
        /// Sort the provided pet list
        /// </summary>
        /// <param name="pets"></param>
        /// <returns></returns>
        List<Pet> SortByName(List<Pet> pets);
    }
}