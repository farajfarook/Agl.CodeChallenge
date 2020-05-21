using System.Collections.Generic;
using AglTest.Domain.Models;

namespace AglTest.Domain.Tests.Mocks
{
    public static class PetsMock
    {
        public static readonly Pet Nick = new Pet
        {
            Name = "Nick",
            Type = PetType.Dog
        };
        
        public static readonly Pet Max = new Pet
        {
            Name = "Max",
            Type = PetType.Dog
        };

        public static readonly Pet Maxi = new Pet
        {
            Name = "Maxi",
            Type = PetType.Cat
        };
        
        public static readonly Pet Kite = new Pet
        {
            Name = "Kite",
            Type = PetType.Cat
        };
        
        public static readonly Pet Spooky = new Pet
        {
            Name = "Spooky",
            Type = PetType.Cat
        };

        public static readonly Pet Mingo = new Pet
        {
            Name = "Mingo",
            Type = PetType.Fish
        };

        public static readonly Pet Hogger = new Pet
        {
            Name = "Hogger",
            Type = PetType.Fish
        };

        public static List<Pet> GetAllValidPets()
        {
            return new List<Pet>{Nick, Max, Maxi, Kite, Spooky, Mingo, Hogger};
        }
        
        public static List<Pet> GetSomeNullValidPets()
        {
            return new List<Pet>{Nick, Max, Maxi, null, Kite, Spooky, null, Mingo, Hogger};
        }
        
        public static List<Pet> GetAllNullPets()
        {
            return new List<Pet>() {null, null, null, null, null};
        }

        public static List<Pet> GetDuplicatedListPets()
        {
            return new List<Pet>{Nick, Max, Maxi, Spooky, Spooky, Mingo, Hogger};
        }
    }
}