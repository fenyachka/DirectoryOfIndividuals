using System;

namespace Application.Persons.DTO
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstNameGeo { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameGeo { get; set; }
        public string LastNameEn { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}