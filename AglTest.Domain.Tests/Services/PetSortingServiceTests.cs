using System;
using System.Linq;
using AglTest.Domain.Pets.Services;
using AglTest.Domain.Tests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace AglTest.Domain.Tests.Services
{
    public class PetSortingServiceTests
    {
        private readonly IPetSortingService _service = new PetSortingService(NullLogger<PetSortingService>.Instance);

        [Fact]
        public void SortByName_AllValidPets_Success()
        {
            var data = PetsMock.GetAllValidPets();                      
            var pets = _service.SortByName(data).ToArray();
            
            data.Sort((pet, pet1) => string.Compare(pet.Name, pet1.Name, StringComparison.Ordinal));

            for (var i = 0; i < pets.Length; i++)
                Assert.Equal(pets[i], data[i]);
            
        }
        
        [Fact]
        public void SortByName_SomeNullValidPets_FilteredValues()
        {
            var data = PetsMock.GetSomeNullValidPets();
            var pets = _service.SortByName(data).ToArray();
            Assert.Equal(PetsMock.Hogger, pets[0]);
            Assert.Equal(PetsMock.Kite, pets[1]);
            Assert.Equal(PetsMock.Spooky, pets[6]);
            Assert.Equal(7, pets.Length);
        }
        
        [Fact]
        public void SortByName_WithAllNullPets_Empty()
        {
            var data = PetsMock.GetAllNullPets();
            var pets = _service.SortByName(data).ToArray();
            Assert.Empty(pets);
        }
        
        [Fact]
        public void SortByName_DuplicatedPets_Success()
        {
            var data = PetsMock.GetDuplicatedListPets();
            var pets = _service.SortByName(data).ToArray();
            Assert.Equal(PetsMock.Hogger, pets.FirstOrDefault());
        }
    }
}