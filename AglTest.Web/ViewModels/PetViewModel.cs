using AglTest.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AglTest.Web.ViewModels
{
    public class PetViewModel
    {
        public PetViewModel(Pet pet)
        {
            Name = pet.Name;
            Type = pet.Type;
        }

        public string Name { get; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public PetType Type { get; }
    }
}