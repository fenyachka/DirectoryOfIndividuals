using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using Domain.Intefaces;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Persons
{
    public class Create
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
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _unitOfWork.Person.Add(request.Person);

                var result = await _unitOfWork.Complete() > 0;

                if(!result) return Result<Unit>.Failure("Failed to create person");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}