using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using Domain.Intefaces;
using MediatR;

namespace Application.Persons
{
    public class AddRelatedPerson
    {
        public class Command:IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public string PrivateNumber { get; set; }
            public int RelationshipId { get; set; }
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
                var relatedPerson = _unitOfWork.Person.Table.FirstOrDefault(x=>x.PrivateNumber==request.PrivateNumber);

                if(relatedPerson==null)
                    return Result<Unit>.Failure("Person with such P/N doesn't exists!");

                if (relatedPerson.Id==request.Id)
                return Result<Unit>.Failure("Can't add self");

                var person = _unitOfWork.Person.Include(x=>x.RelatedPeople).SingleOrDefault(x => x.Id == request.Id);
                if(person.RelatedPeople.Any(x=>x.RelatedPersonId==relatedPerson.Id))
                return Result<Unit>.Failure("This Person already added");

                else
                {
                var relPerson = new RelatedPeople
                    {
                        PersonId = request.Id,
                        RelatedPersonId = relatedPerson.Id,
                        RelationshipId = request.RelationshipId
                    };

                    person.RelatedPeople.Add(relPerson);

                    var result = await _unitOfWork.Complete() > 0;

                    return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to add related person");
                }
            }
        }
    }
}