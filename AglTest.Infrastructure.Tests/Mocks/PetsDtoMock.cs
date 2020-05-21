using System.Collections.Generic;
using AglTest.Domain.Models;
using AglTest.Infrastructure.Data;

namespace AglTest.Infrastructure.Tests.Mocks
{
    public static class PetsDtoMock
    {
        public static readonly PetDto Nick = new PetDto
        {
            Name = "Nick",
            Type = PetType.Dog
        };
        
        public static readonly PetDto Max = new PetDto
        {
            Name = "Max",
            Type = PetType.Dog
        };

        public static readonly PetDto Maxi = new PetDto
        {
            Name = "Maxi",
            Type = PetType.Cat
        };
        
        public static readonly PetDto Kite = new PetDto
        {
            Name = "Kite",
            Type = PetType.Cat
        };
        
        public static readonly PetDto Spooky = new PetDto
        {
            Name = "Spooky",
            Type = PetType.Cat
        };

        public static readonly PetDto Mingo = new PetDto
        {
            Name = "Mingo",
            Type = PetType.Fish
        };

        public static readonly PetDto Hogger = new PetDto
        {
            Name = "Hogger",
            Type = PetType.Fish
        };

        public static IEnumerable<PetDto> GetAllValidPets()
        {
            return new[] {Nick, Max, Maxi, Kite, Spooky, Mingo, Hogger};
        }
        
        public static IEnumerable<PetDto> GetSomeNullValidPets()
        {
            return new[] {Nick, Max, Maxi, null, Kite, Spooky, null, Mingo, Hogger};
        }
        
        public static IEnumerable<PetDto> GetAllNullPets()
        {
            return new List<PetDto>() {null, null, null, null, null};
        }

        public static IEnumerable<PetDto> GetDuplicatedListPets()
        {
            return new[] {Nick, Max, Maxi, Spooky, Spooky, Mingo, Hogger};
        }
    }
}