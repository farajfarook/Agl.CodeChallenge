using AglTest.Domain.Models;
using AglTest.Domain.Pets.Models;
using AglTest.Infrastructure.Data;
using AglTest.Infrastructure.Mappers;
using Xunit;

namespace AglTest.Infrastructure.Tests.Mappers
{
    public class PetMapperTests
    {
        [Theory]
        [InlineData("Miya", PetType.Cat)]
        [InlineData("Rofl", PetType.Dog)]
        [InlineData(null, PetType.Cat)]
        [InlineData(null, null)]
        public void Map_Theory(string name, PetType? type)
        {
            var mapper = new PetMapper();
            var model = mapper.Map(new PetDto
            {
                Name = name,
                Type = type
            });
            
            Assert.Equal(name, model.Name);
            Assert.Equal(type??PetType.Other, model.Type);
        }
    }
}