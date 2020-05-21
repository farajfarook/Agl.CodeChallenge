using System;
using AglTest.Domain.Pets.Services;
using AglTest.Domain.Tests.Mocks;
using Castle.Core.Internal;
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
            Assert.True(pets[0] == PetsMock.Hogger);
            Assert.True(pets[1] == PetsMock.Kite);
            Assert.True(pets[6] == PetsMock.Spooky);
            Assert.True(pets.Length == 7);
        }
        
        [Fact]
        public void SortByName_WithAllNullPets_Empty()
        {
            var data = PetsMock.GetAllNullPets();
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets.IsNullOrEmpty());
        }
        
        [Fact]
        public void SortByName_DuplicatedPets_Success()
        {
            var data = PetsMock.GetDuplicatedListPets();
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets[0] == PetsMock.Hogger);
        }
    }
}