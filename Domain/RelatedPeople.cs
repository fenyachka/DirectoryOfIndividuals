using System;

namespace Domain
{
    public class RelatedPeople:BaseEntity
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public Guid RelatedPersonId { get; set; }
        public Person RelatedPerson { get; set; }

        public int RelationshipId { get; set; }
        public Relationship Relationship { get; set; }
    }
}