using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Persons.DTO;
using Domain.Intefaces;
using MediatR;

namespace Application.Persons
{
    public class ListRelatedPeople
    {
        public class Query : IRequest<Result<List<RelatedPersonDto>>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<RelatedPersonDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<RelatedPersonDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var person =  await _unitOfWork.Person.GetByIdAsync(request.Id);
                
                if(person != null) {
                var relatedPersonsList = _unitOfWork.RelatedPeople.Table.Where(y=>y.PersonId==request.Id).Select(x=>new RelatedPersonDto
                {
                    Id=x.RelatedPersonId,
                    PrivateNumber=x.RelatedPerson.PrivateNumber,
                    Birthdate=x.RelatedPerson.Birthdate,
                    FirstNameGeo = x.RelatedPerson.FirstNameGeo,
                    LastNameGeo=x.RelatedPerson.LastNameGeo,
                    Relationship=x.Relationship.Definition
                }).ToList();

                return Result<List<RelatedPersonDto>>.Success(relatedPersonsList);
                }

                return null;
            }

        }
    }
}