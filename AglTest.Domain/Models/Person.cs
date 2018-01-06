using System.Collections.Generic;
using AglTest.Domain.SeedWork;

namespace AglTest.Domain.Models
{
    public class Person: IRootModel
    {
        public PersonGender Gender { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IEnumerable<Pet> Pets { get; set; }        
    }
}