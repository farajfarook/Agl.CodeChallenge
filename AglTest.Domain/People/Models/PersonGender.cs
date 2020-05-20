using System.Text.Json.Serialization;

namespace AglTest.Domain.People.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PersonGender
    {
        Male, Female, Other
    }
}