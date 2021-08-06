using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Persons.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.Intefaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Persons
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<PersonDto>>>
        {
            public PersonParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PersonDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<PersonDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _unitOfWork.Person.Table
                        .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                        .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Params.PrivateNumber))
                {
                    query = query.Where(x => x.PrivateNumber.Contains(request.Params.PrivateNumber));
                }
                if (!string.IsNullOrWhiteSpace(request.Params.FirstnameGeo))
                {
                    query = query.Where(x => x.FirstNameGeo.Contains(request.Params.FirstnameGeo));
                }
                if (!string.IsNullOrWhiteSpace(request.Params.LastnameGeo))
                {
                    query = query.Where(x => x.LastNameGeo.Contains(request.Params.LastnameGeo));
                }
                if (!string.IsNullOrWhiteSpace(request.Params.FirstnameEn))
                {
                    query = query.Where(x => x.FirstNameEn.Contains(request.Params.FirstnameEn));
                }
                if (!string.IsNullOrWhiteSpace(request.Params.LastnameEn))
                {
                    query = query.Where(x => x.LastNameEn.Contains(request.Params.LastnameEn));
                }
                if (request.Params.Birthdate != null)
                {
                    query = query.Where(x => x.Birthdate == request.Params.Birthdate);
                }
                if (!string.IsNullOrWhiteSpace(request.Params.Address))
                {
                    query = query.Where(x => x.Address.Contains(request.Params.Address));
                }
                if (!string.IsNullOrWhiteSpace(request.Params.Phone))
                {
                    query = query.Where(x => x.Phone.Contains(request.Params.Phone));
                }
                if (!string.IsNullOrWhiteSpace(request.Params.Email))
                {
                    query = query.Where(x => x.Email.Contains(request.Params.Email));
                }
                return Result<PagedList<PersonDto>>.Success(
                    await PagedList<PersonDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                    );
            }

        }
    }
}