using System.Collections.Generic;
using AglTest.Domain.Models;
using AglTest.Domain.People.Models;

namespace AglTest.Domain.Tests.Mocks
{
    public static class People
    {
        private static readonly Person Sam = new Person()
        {
            Age = 5,
            Gender = PersonGender.Male,
            Name = "Sam",
            Pets = new List<string>
            {
                Pets.Max.Name,
                Pets.Maxi.Name
            }
        };

        private static readonly Person Anne = new Person()
        {
            Age = 5,
            Gender = PersonGender.Female,
            Name = "Anne",
            Pets = new List<string>
            {
                Pets.Nick.Name,
                Pets.Hogger.Name
            }
        };
        
        private static readonly Person Jenny = new Person()
        {
            Age = 5,
            Gender = PersonGender.Female,
            Name = "Jenny",
            Pets = new List<string>()
            {
                Pets.Kite.Name,
                Pets.Spooky.Name,
                Pets.Mingo.Name,
                Pets.Hogger.Name
            }
        };
        
        private static readonly Person Wini = new Person()
        {
            Age = 5,
            Gender = PersonGender.Female,
            Name = "Wini",
            Pets = new List<string>()
        };
        
        private static readonly Person Raju = new Person()
        {
            Age = 5,
            Gender = PersonGender.Male,
            Name = "Raju",
            Pets = null
        };
        
        private static readonly Person Stev = new Person()
        {
            Age = 5,
            Gender = PersonGender.Male,
            Name = "Steve",
            Pets = new List<string>
            {
                Pets.Hogger.Name
            }
        };
        
        
        public static IEnumerable<Person> GetAllValidPeople()
        {
            return new List<Person>() {Sam, Anne, Jenny, Raju, Wini, Stev};
        }

        public static IEnumerable<Person> GetAllNullPeople()
        {
            return new List<Person>() {null, null, null, null, null, null};
        }

        public static IEnumerable<Person> GetSomeNullPeople()
        {
            return new List<Person>() {null, Sam, Anne, Jenny, null, Raju, Wini, Stev, null};
        }

        public static IEnumerable<Person> GetAllEmptyPetPeople()
        {
            return new List<Person>() {Raju, Wini};
        }

        public static IEnumerable<Person> GetSharedPetPeople()
        {
            return new List<Person>() {Anne, Stev, Jenny};
        }
    }
}