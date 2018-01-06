using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.Services;
using AglTest.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AglTest.Web.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private readonly IPetCollectionService _petCollectionService;

        public PetsController(IPetCollectionService petCollectionService)
        {
            _petCollectionService = petCollectionService;
        }

        // GET api/Pets
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pets = await _petCollectionService.ListSortedPetsByGenderAsync();
            var petCollections = pets.Select(tuple => new PetCollectionViewModel(tuple.Item1, tuple.Item2)).ToList();
            return Ok(petCollections);
        }
    }
}