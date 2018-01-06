using System.Linq;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.Repositories;
using AglTest.Domain.Services;
using AglTest.Domain.Tests.Mocks;
using Castle.Core.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AglTest.Domain.Tests.Services
{
    public class PetSortingServiceTests
    {
        private readonly IPetSortingService _service = new PetUtilService(Mock.Of<ILogger<PetUtilService>>());
                

        [Fact]
        public void SortByName_WithAllValidPets()
        {
            var data = Pets.GetAllValidPets();                      
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets[0] == Pets.Hogger);
        }
        
        [Fact]
        public void SortByName_WithSomeNullValidPets()
        {
            var data = Pets.GetSomeNullValidPets();
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets[0] == Pets.Hogger);
            Assert.True(pets.Length == 7);
        }
        
        [Fact]
        public void SortByName_WithAllNullPets()
        {
            var data = Pets.GetAllNullPets();
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets.IsNullOrEmpty());
        }
        
        [Fact]
        public void SortByName_WithDuplicatedPets()
        {
            var data = Pets.GetDuplicatedListPets();
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets[0] == Pets.Hogger);
        }
    }
}