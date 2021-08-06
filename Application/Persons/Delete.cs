using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain.Intefaces;
using MediatR;
using Persistence;

namespace Application.Persons
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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
                var person = await _unitOfWork.Person.GetByIdAsync(request.Id);

                if(person == null) return null;

                _unitOfWork.Person.Delete(person);

                var result = await _unitOfWork.Complete() > 0;

                if(!result) return Result<Unit>.Failure("Failed to delete person");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}