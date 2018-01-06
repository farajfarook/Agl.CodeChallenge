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
        private IPetSortingService _service;
        private readonly ILogger<PetService> _serviceLogger = Mock.Of<ILogger<PetService>>();        

        [Fact]
        public void SortByName_WithAllValidPets()
        {
            var data = Pets.GetAllValidPets();
            var repoMock = new Mock<IPersonRepository>();            
            _service = new PetService(repoMock.Object, _serviceLogger);   
            
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets[0] == Pets.Hogger);
        }
        
        [Fact]
        public void SortByName_WithSomeNullValidPets()
        {
            var data = Pets.GetSomeNullValidPets();
            var repoMock = new Mock<IPersonRepository>();            
            _service = new PetService(repoMock.Object, _serviceLogger);   
            
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets[0] == Pets.Hogger);
            Assert.True(pets.Length == 7);
        }
        
        [Fact]
        public void SortByName_WithAllNullPets()
        {
            var data = Pets.GetAllNullPets();
            var repoMock = new Mock<IPersonRepository>();            
            _service = new PetService(repoMock.Object, _serviceLogger);   
            
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets.IsNullOrEmpty());
        }
        
        [Fact]
        public void SortByName_WithDuplicatedPets()
        {
            var data = Pets.GetDuplicatedListPets();
            var repoMock = new Mock<IPersonRepository>();            
            _service = new PetService(repoMock.Object, _serviceLogger);   
            
            var pets = _service.SortByName(data).ToArray();
            Assert.True(pets[0] == Pets.Hogger);
        }
    }
}