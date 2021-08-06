using System;

namespace Application.Persons.DTO
{
    public class RelatedPersonDto
    {
        public Guid Id { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstNameGeo { get; set; }
        public string LastNameGeo { get; set; }
        public DateTime Birthdate { get; set; }
        public string Relationship { get; set; }
    }
}