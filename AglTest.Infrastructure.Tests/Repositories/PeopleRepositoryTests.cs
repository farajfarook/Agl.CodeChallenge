using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain.People;
using AglTest.Domain.People.Models;
using AglTest.Infrastructure.Data;
using AglTest.Infrastructure.Mappers;
using AglTest.Infrastructure.Repositories;
using AglTest.Infrastructure.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace AglTest.Infrastructure.Tests.Repositories
{
    public class PeopleRepositoryTests
    {
        private readonly IServiceProvider _provider;

        public PeopleRepositoryTests()
        {
            var services = new ServiceCollection();
            services.AddSingleton(Substitute.For<IPersonMapper>());
            services.AddSingleton(Substitute.For<IDataService>());
            services.AddSingleton<IPeopleRepository, PeopleRepository>();
            _provider = services.BuildServiceProvider();
        }
        
        [Fact]
        public async Task ListAsyncTest_Success()
        {
            var repo = _provider.GetService<IPeopleRepository>();
            var dataService = _provider.GetService<IDataService>();
            var mapper = _provider.GetService<IPersonMapper>();
            
            var dtos = PeopleDtoMock.GetAllValidPeople();

            dataService.FetchAsync(Arg.Is(CancellationToken.None))
                .Returns(Task.FromResult(dtos));

            mapper.Map(Arg.Any<PersonDto>()).Returns(new Person());
            
            var resp = await repo.ListAsync(CancellationToken.None);

            foreach (var personDto in dtos)
            {
                mapper.Received().Map(personDto);
            }
            
            Assert.NotNull(resp);
            Assert.Equal(dtos.Count, resp.Count());
        }
        
        [Fact]
        public async Task ListAsyncTest_DataServiceFails()
        {
            var repo = _provider.GetService<IPeopleRepository>();
            var mapper = _provider.GetService<IPersonMapper>();

            mapper.Map(Arg.Any<PersonDto>()).Returns(new Person());
            
            var resp = await repo.ListAsync(CancellationToken.None);

            mapper.DidNotReceive().Map(Arg.Any<PersonDto>());
            Assert.NotNull(resp);
            Assert.Equal(0, resp.Count());
        }

        [Theory]
        [InlineData(PersonGender.Male)]
        [InlineData(PersonGender.Female)]
        [InlineData(PersonGender.Other)]
        [InlineData(null)]
        public async Task ListByGenderAsync_Theory(PersonGender gender)
        {
            var repo = _provider.GetService<IPeopleRepository>();
            var dataService = _provider.GetService<IDataService>();

            var dtos = PeopleDtoMock.GetAllValidPeople();

            dataService.FetchAsync(Arg.Is(CancellationToken.None))
                .Returns(Task.FromResult(dtos));

            var resp = await repo.ListByGenderAsync(gender, CancellationToken.None);
            Assert.NotNull(resp);
            Assert.Equal(dtos.Count(d => d.Gender == gender), resp.Count());
        }
    }
}