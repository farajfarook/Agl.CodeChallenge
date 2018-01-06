using System.Collections.Generic;
using AglTest.Domain.Models;

namespace AglTest.Domain.Services
{
    public interface IPetSortingService
    {
        IEnumerable<Pet> SortByName(IEnumerable<Pet> pets);
    }
}