using System;
using System.Collections.Generic;
using System.Linq;
using AglTest.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AglTest.Web.ViewModels
{
    public class PetCollectionViewModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PersonGender Gender { get; }
        
        public IEnumerable<PetViewModel> Pets { get; }

        public PetCollectionViewModel(PersonGender gender, IEnumerable<Pet> pets)
        {
            Gender = gender;
            Pets = pets.Select(p => new PetViewModel(p));
        }
    }
}