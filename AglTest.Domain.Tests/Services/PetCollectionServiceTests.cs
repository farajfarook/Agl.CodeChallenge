using System;
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
    public class PetCollectionServiceTests
    {
        private IPetCollectionService _service;
        private readonly ILogger<PetDataService> _dataServiceLogger = Mock.Of<ILogger<PetDataService>>();
        private readonly PetUtilService _petUtilService = new PetUtilService(Mock.Of<ILogger<PetUtilService>>());

        [Fact]
        public Task ListPetsByPersonGenderAsync_AllValidPeople_Success()
        {
            return ListPetsByPersonGenderAsync_WithData(People.GetAllValidPeople(), 3, 5, 0);
        }
        
        [Fact]
        public Task ListPetsByPersonGenderAsync_AllNullPeople_Success()
        {
            return ListPetsByPersonGenderAsync_WithData(People.GetAllNullPeople(), 0, 0, 0);
        }
        
        [Fact]
        public Task ListPetsByPersonGenderAsync_SomeNullPeople_Success()
        {
            return ListPetsByPersonGenderAsync_WithData(People.GetSomeNullPeople(), 3, 5, 0);
        }

        private async Task ListPetsByPersonGenderAsync_WithData(IEnumerable<Person> data, int maleCount, int femaleCount, int otherCount)
        {            
            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(_ => _.ListAsync()).ReturnsAsync(data);
            _service = new PetDataService(repoMock.Object, _dataServiceLogger, _petUtilService);   
            
            var petCollection = await _service.ListSortedPetsByGenderAsync();
            var petTuples = petCollection as Tuple<PersonGender, IEnumerable<Pet>>[] ?? petCollection.ToArray();
            
            foreach (var petTuple in petTuples)
            {
                var pets = petTuple.Item2.ToArray();
                switch (petTuple.Item1)
                {
                    case PersonGender.Male:
                        Assert.True(pets.Count() == maleCount);
                        Assert.True(pets[0] == Pets.Hogger);
                        break;
                    case PersonGender.Female:
                        Assert.True(pets.Count() == femaleCount);
                        Assert.True(pets[0] == Pets.Hogger);
                        break;
                    default:
                        Assert.True(pets.Count() == otherCount);
                        break;
                }
            }   
        }
    }
}