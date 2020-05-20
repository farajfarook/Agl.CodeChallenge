using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.Pets;
using Enbiso.NLib.Cqrs;
using Enbiso.NLib.DependencyInjection;
using FluentValidation;

namespace AglTest.Api.Application.Pets.Handlers
{
    public class ListPets: ICommand<ListPetsResponse>
    {
        public string OwnerName { get; set; }
    }

    public class ListPetsResponse : ICommandResponse
    {
        public IEnumerable<Pet> Records { get; set; }
        public long Count { get; set; }
    }

    [TransientService]
    public class ListPetsValidator : AbstractValidator<ListPets>, ICommandValidator<ListPets>
    {
        public ListPetsValidator()
        {
            RuleFor(r => r.OwnerName).NotEmpty().WithMessage("owner cannot be empty");
        }

        public new async Task<IEnumerable<ValidationError>> Validate(ListPets command)
            => (await ValidateAsync(command)).Errors.Select(e => new ValidationError(e.ErrorMessage, e.PropertyName));
    }
    
    [TransientService]
    public class ListPetsHandler : ICommandHandler<ListPets, ListPetsResponse>
    {
        private readonly IPetsRepository _repository;

        public ListPetsHandler(IPetsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListPetsResponse> Handle(ListPets request, CancellationToken cancellationToken)
        {
            var records = await _repository.ListAsync(request.OwnerName, cancellationToken);
            var pets = records as Pet[] ?? records.ToArray();
            return new ListPetsResponse
            {
                Count = pets.Length,
                Records = pets
            };
        }
    }
}