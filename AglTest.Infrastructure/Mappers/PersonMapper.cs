using System.Collections.Generic;
using System.Linq;
using AglTest.Domain.People.Models;
using AglTest.Infrastructure.Data;
using Enbiso.NLib.DependencyInjection;

namespace AglTest.Infrastructure.Mappers
{
    public interface IPersonMapper
    {
        Person Map(PersonDto dto);
    }
    
    [TransientService]
    public class PersonMapper: IPersonMapper
    {
        public Person Map(PersonDto dto)
        {
            return new Person
            {
                Age = dto.Age,
                Gender = dto.Gender,
                Name = dto.Name,
                Pets = dto.Pets?.Select(p => p.Name)?? new List<string>()
            };
        }
    }
}