using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain.Intefaces;
using MediatR;

namespace Application.Persons
{
    public class RemoveRelatedPerson
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public Guid RelatedPersonId { get; set; }
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
                var relatedPerson = _unitOfWork.RelatedPeople.Table.FirstOrDefault(x => x.PersonId == request.Id && x.RelatedPersonId == request.RelatedPersonId);

                if (relatedPerson == null) return null;

                _unitOfWork.RelatedPeople.Delete(relatedPerson);

                var result = await _unitOfWork.Complete() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the related person");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}