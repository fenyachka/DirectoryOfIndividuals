using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Persons;
using Application.Persons.DTO;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PersonsController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPersons([FromQuery]PersonParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query {Params = param}));
        }

        [HttpGet("{personId}/related")]
        public async Task<ActionResult<List<RelatedPersonDto>>> GetRelatedPersons(Guid personId)
        {
            return HandleResult(await Mediator.Send(new ListRelatedPeople.Query {Id = personId}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(Guid id) 
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            return HandleResult(await Mediator.Send(new Create.Command {Person = person}));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddRelatedPerson(Guid id, RelatedPersonIdentificatorDto dto)
        {
            return HandleResult(await Mediator.Send(new AddRelatedPerson.Command {Id = id,PrivateNumber=dto.PrivateNumber,RelationshipId=dto.RelationshipId}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPerson(Guid id, Person person)
        {
            person.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command{Person = person}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));
        }

        [HttpPost("{id}/dlr")]
        public async Task<IActionResult> DeleteRelatedPerson(Guid id, RelatedPersonRemoveDto dto)
        {
            return HandleResult(await Mediator.Send(new RemoveRelatedPerson.Command{Id=id,RelatedPersonId=dto.RelatedPersonId}));
        }
    }
}
