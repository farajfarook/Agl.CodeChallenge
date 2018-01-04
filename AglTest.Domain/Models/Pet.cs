using AglTest.Domain.SeedWork;

namespace AglTest.Domain.Models
{
    public class Pet: IModel
    {
        public PetType Type { get; set; }
        public string Name { get; set; }
    }
}