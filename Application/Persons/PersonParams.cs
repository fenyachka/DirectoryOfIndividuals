using System;
using Application.Core;

namespace Application.Persons
{
    public class PersonParams : PagingParams
    {
        public string PrivateNumber { get; set; }
        public string FirstnameGeo { get; set; }
        public string LastnameGeo { get; set; }
        public string FirstnameEn { get; set; }

        public string LastnameEn { get; set; }
        public DateTime? Birthdate { get; set; } = null;
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}