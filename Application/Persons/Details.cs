using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Persons.DTO;
using AutoMapper;
using Domain;
using Domain.Intefaces;
using MediatR;
using Persistence;

namespace Application.Persons
{
    public class Details
    {
        public class Query : IRequest<Result<Person>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Person>>
        {
            private readonly IUnitOfWork _unitOfWork;
             private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            { 
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<Person>> Handle(Query request, CancellationToken cancellationToken)
            {
                var person =  await _unitOfWork.Person.GetByIdAsync(request.Id);
                
                if(person == null) {
                    return Result<Person>.Failure("Person with such Id doesn't exists");
                }
                
                return Result<Person>.Success(person);
            }
        }
    }
}