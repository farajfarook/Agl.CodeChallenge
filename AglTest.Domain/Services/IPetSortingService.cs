using System.Collections.Generic;
using AglTest.Domain.Models;

namespace AglTest.Domain.Services
{
    public interface IPetSortingService
    {
        /// <summary>
        /// Sort the provided pet list
        /// </summary>
        /// <param name="pets"></param>
        /// <returns></returns>
        IEnumerable<Pet> SortByName(IEnumerable<Pet> pets);
    }
}