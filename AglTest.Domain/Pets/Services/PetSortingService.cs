using System.Collections.Generic;
using System.Linq;
using AglTest.Domain.Models;
using Enbiso.NLib.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AglTest.Domain.Pets.Services
{
    [TransientService]
    public class PetSortingService: IPetSortingService
    {
        private readonly ILogger _logger;

        public PetSortingService(ILogger<PetSortingService> logger)
        {
            _logger = logger;
        }

        public List<Pet> SortByName(List<Pet> pets)
        {
            _logger.LogTrace("Sort by name");
            var petArray = pets ?? new List<Pet>();
            petArray.RemoveAll(m => m == null);
            return petArray.OrderBy(m => m?.Name).ToList();
        }
    }
}