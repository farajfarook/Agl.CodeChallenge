using System.Collections.Generic;

namespace AglTest.Domain.People.Models
{
    public class Person
    {
        public PersonGender Gender { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IEnumerable<string> Pets { get; set; }        
    }
}