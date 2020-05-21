using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain;
using AglTest.Domain.Models;
using AglTest.Domain.People;
using AglTest.Domain.People.Models;
using AglTest.Domain.Pets;
using AglTest.Infrastructure.Data;
using AglTest.Infrastructure.Mappers;
using AglTest.Infrastructure.Repositories;
using AglTest.Infrastructure.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace AglTest.Infrastructure.Tests.Repositories
{
    public class PetsRepositoryTests
    {
        private readonly IServiceProvider _provider;

        public PetsRepositoryTests()
        {
            var services = new ServiceCollection();
            services.AddSingleton(Substitute.For<IPetMapper>());
            services.AddSingleton(Substitute.For<IDataService>());
            services.AddSingleton<IPetsRepository, PetsRepository>();
            _provider = services.BuildServiceProvider();
        }
        
        [Fact]
        public async Task ListAsyncTest_Success()
        {
            var repo = _provider.GetService<IPetsRepository>();
            var dataService = _provider.GetService<IDataService>();
            var mapper = _provider.GetService<IPetMapper>();
            
            var dtos = PeopleDtoMock.GetAllValidPeople();

            dataService.FetchAsync(Arg.Is(CancellationToken.None))
                .Returns(Task.FromResult(dtos));

            mapper.Map(Arg.Any<PetDto>()).Returns(new Pet());

            foreach (var dto in dtos)
            {
                var resp = await repo.ListAsync(dto.Name, CancellationToken.None);
                foreach (var petDto in dto.Pets)
                {
                    mapper.Received().Map(petDto);
                }
                Assert.NotNull(resp);
                Assert.Equal(dto.Pets?.Count(), resp.Count());
            }
        }
        
        [Fact]
        public async Task ListAsyncTest_InvalidName()
        {
            var repo = _provider.GetService<IPetsRepository>();
            var mapper = _provider.GetService<IPetMapper>();

            mapper.Map(Arg.Any<PetDto>()).Returns(new Pet());
            await Assert.ThrowsAsync<NotFoundException>(() => repo.ListAsync("testName", CancellationToken.None));
        }
    }
}