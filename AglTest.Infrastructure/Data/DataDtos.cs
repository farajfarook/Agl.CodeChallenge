using System.Collections.Generic;
using System.Text.Json.Serialization;
using AglTest.Domain.Models;
using AglTest.Domain.People.Models;

namespace AglTest.Infrastructure.Data
{
    public class PersonDto
    {
        [JsonPropertyName("gender")]
        public PersonGender Gender { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("pets")]
        public IEnumerable<PetDto> Pets { get; set; }
    }

    public class PetDto
    {
        [JsonPropertyName("type")]
        public PetType Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}