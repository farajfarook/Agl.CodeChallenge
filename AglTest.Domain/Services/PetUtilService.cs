using System.Collections.Generic;
using System.Linq;
using AglTest.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AglTest.Domain.Services
{
    public class PetUtilService: IPetSortingService
    {
        private readonly ILogger _logger;

        public PetUtilService(ILogger<PetUtilService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Pet> SortByName(IEnumerable<Pet> pets)
        {
            _logger.LogTrace("Sort by name");
            var petArray = pets as List<Pet> ?? pets.ToList();
            petArray.RemoveAll(m => m == null);
            return petArray.OrderBy(m => m?.Name).ToList();
        }
    }
}