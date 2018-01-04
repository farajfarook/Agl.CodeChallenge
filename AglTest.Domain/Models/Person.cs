using System.Collections.Generic;
using AglTest.Domain.SeedWork;

namespace AglTest.Domain.Models
{
    public class Person: IModel
    {
        public PersonGender Gender { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IEnumerable<Pet> Pets { get; set; }
        
    }

    public class Pet: IModel
    {
        public PetType Type { get; set; }
        public string Name { get; set; }
    }

    public enum PetType
    {
        Cat, Dog
    }

    public enum PersonGender
    {
        Male, Female, Other
    }
}