using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using Domain.Intefaces;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Persons
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Person Person { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(X => X.Person).SetValidator(new PersonValidator());
            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
             private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var person = await  _unitOfWork.Person.GetByIdAsync(request.Person.Id);

                if(person == null) return null;

                //_mapper.Map(request.Person, person);

                else
                {
                    person.FirstNameEn = request.Person.FirstNameEn;
                    person.LastNameEn = request.Person.LastNameEn;
                    person.FirstNameGeo = request.Person.FirstNameGeo;
                    person.LastNameGeo = request.Person.LastNameGeo;
                    person.PrivateNumber = request.Person.PrivateNumber;
                    person.Birthdate = request.Person.Birthdate;
                    person.Address = request.Person.Address;
                    person.Phone = request.Person.Phone;
                    person.Email = request.Person.Email;
                }
                
                _unitOfWork.Person.Update(person);

                var result = await _unitOfWork.Complete() > 0;

                if(!result) return Result<Unit>.Failure("Failed to update person");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}