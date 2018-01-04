using System.Collections.Generic;
using AglTest.Domain.Models;

namespace AglTest.Domain.Services
{
    public interface IPetService
    {
        IEnumerable<Pet> SortByName(IEnumerable<Pet> pets);
        IEnumerable<Pet> FetchPetsByGender(IEnumerable<Person> persons, PersonGender gender);
    }
}