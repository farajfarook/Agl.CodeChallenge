using System.Text.Json.Serialization;

namespace AglTest.Domain.Pets.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PetType
    {
        Cat, Dog, Fish, Other
    }
}