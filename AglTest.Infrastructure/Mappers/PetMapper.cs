using AglTest.Domain.Models;
using AglTest.Infrastructure.Data;
using Enbiso.NLib.DependencyInjection;

namespace AglTest.Infrastructure.Mappers
{
    public interface IPetMapper
    {
        Pet Map(PetDto dto);
    }
    
    [TransientService]
    public class PetMapper: IPetMapper
    {
        public Pet Map(PetDto dto)
        {
            return new Pet
            {
                Name = dto.Name,
                Type = dto.Type
            };
        }
    }
}