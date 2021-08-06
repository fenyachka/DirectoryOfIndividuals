using System;
using System.Collections.Generic;

namespace Domain
{
    public class Person : BaseEntity
    {
        public string FirstNameGeo { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameGeo { get; set; }
        public string LastNameEn { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        private readonly List<RelatedPeople> _relatedPeople = new List<RelatedPeople>();
        public ICollection<RelatedPeople> RelatedPeople => _relatedPeople;

        public void AddRelatedPerson(Guid personId, Guid relatedPersonId, int relationalshipId)
        {
            var relatedPerson = new RelatedPeople
            {
                PersonId=personId,
                RelatedPersonId=relatedPersonId,
                RelationshipId=relationalshipId
            };


            _relatedPeople.Add(relatedPerson);
        }


        public void RemoveRelatedPerson(Guid personId, Guid relatedPersonId)
        {
            var relatedPerson = _relatedPeople.Find(x => x.PersonId==personId && x.RelatedPersonId==relatedPersonId);
            _relatedPeople.Remove(relatedPerson);
        }
        
    }
}