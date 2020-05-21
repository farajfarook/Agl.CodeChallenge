using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.People.Models;
using AglTest.Infrastructure.Data;
using AglTest.Infrastructure.Mappers;
using Xunit;

namespace AglTest.Infrastructure.Tests.Mappers
{
    public class PersonMapperTests
    {
        [Theory]
        [InlineData("Faraj", PersonGender.Male, 33, 3)]
        [InlineData("Sam", PersonGender.Female, 33, 0)]
        [InlineData("John", PersonGender.Other, 33, null)]
        [InlineData("Foo", null, -33, 0)]
        [InlineData(null, null, null, null)]
        public void Map_Theory(string name, PersonGender gender, int? age, int? numberOfPets)
        {
            var mapper = new PersonMapper();
            var pets = new List<PetDto>();
            
            if (numberOfPets == null) pets = null;
            else
            {
                for (var i = 0; i < numberOfPets; i++)
                {
                    pets.Add(new PetDto {Name = $"name{i}"});
                }
            }

            var model = mapper.Map(new PersonDto
            {
                Name = name,
                Gender = gender,
                Age = age,
                Pets = pets
            });
            
            Assert.Equal(name, model.Name);
            Assert.NotNull(model.Age);
            Assert.Equal(age??0, model.Age);
            Assert.Equal(gender, model.Gender);
            Assert.NotNull(model.Pets);
            Assert.Equal(numberOfPets??0, model.Pets.Count());
        }
    }
}