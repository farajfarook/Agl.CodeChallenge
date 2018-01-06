using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.Repositories;
using AglTest.Domain.Services;
using AglTest.Domain.Tests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AglTest.Domain.Tests.Services
{
    public class PetFilteringServiceTests
    {
        private IPetFilteringService _service;
        private readonly ILogger<PetDataService> _dataServiceLogger = Mock.Of<ILogger<PetDataService>>();
        private readonly PetUtilService _petUtilService = new PetUtilService(Mock.Of<ILogger<PetUtilService>>());

        [Theory]
        [InlineData(PersonGender.Male, 3)]
        [InlineData(PersonGender.Female, 5)]
        [InlineData(PersonGender.Other, 0)]        
        public async Task ListPetsByPersonGenderAsync_WithAllValidPeople(PersonGender gender, int count)
        {
            var data = People.GetAllValidPeople();
            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(_ => _.ListAsync()).ReturnsAsync(data);
            _service = new PetDataService(repoMock.Object, _dataServiceLogger, _petUtilService);   
            
            var pets = await _service.ListPetsByPersonGenderAsync(gender);
            Assert.True(pets.Count() == count);
        }
        
        [Theory]
        [InlineData(PersonGender.Male, 3)]
        [InlineData(PersonGender.Female, 5)]
        [InlineData(PersonGender.Other, 0)]        
        public async Task ListPetsByPersonGenderAsync_WithSomeNullPeople(PersonGender gender, int count)
        {
            var data = People.GetSomeNullPeople();
            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(_ => _.ListAsync()).ReturnsAsync(data);
            _service = new PetDataService(repoMock.Object, _dataServiceLogger, _petUtilService);   
            
            var pets = await _service.ListPetsByPersonGenderAsync(gender);
            Assert.True(pets.Count() == count);
        }
        
        [Theory]
        [InlineData(PersonGender.Male, 0)]
        [InlineData(PersonGender.Female, 0)]
        [InlineData(PersonGender.Other, 0)]        
        public async Task ListPetsByPersonGenderAsync_WithAllNullPeople(PersonGender gender, int count)
        {
            var data = People.GetAllNullPeople();
            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(_ => _.ListAsync()).ReturnsAsync(data);
            _service = new PetDataService(repoMock.Object, _dataServiceLogger, _petUtilService);   
            
            var pets = await _service.ListPetsByPersonGenderAsync(gender);
            Assert.True(pets.Count() == count);
        }
        
        [Theory]
        [InlineData(PersonGender.Male, 0)]
        [InlineData(PersonGender.Female, 0)]
        [InlineData(PersonGender.Other, 0)]        
        public async Task ListPetsByPersonGenderAsync_WithAllEmptyPetsPeople(PersonGender gender, int count)
        {
            var data = People.GetAllEmptyPetPeople();
            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(_ => _.ListAsync()).ReturnsAsync(data);
            _service = new PetDataService(repoMock.Object, _dataServiceLogger, _petUtilService);   
            
            var pets = await _service.ListPetsByPersonGenderAsync(gender);
            Assert.True(pets.Count() == count);
        }
        
        [Theory]        
        [InlineData(PersonGender.Male, 1)]
        [InlineData(PersonGender.Female, 5)]
        [InlineData(PersonGender.Other, 0)]
        public async Task ListPetsByPersonGenderAsync_WithSharedPetsPeople(PersonGender gender, int count)
        {
            var data = People.GetSharedPetPeople();
            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(_ => _.ListAsync()).ReturnsAsync(data);
            _service = new PetDataService(repoMock.Object, _dataServiceLogger, _petUtilService);   
            
            var pets = await _service.ListPetsByPersonGenderAsync(gender);
            Assert.True(pets.Count() == count);
        }
    }
}