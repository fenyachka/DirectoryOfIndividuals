using Domain;
using FluentValidation;

namespace Application.Persons
{
     public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FirstNameGeo).NotEmpty();
            RuleFor(x => x.FirstNameEn).NotEmpty();
            RuleFor(x => x.LastNameGeo).NotEmpty();
            RuleFor(x => x.LastNameEn).NotEmpty();
            RuleFor(x => x.PrivateNumber).NotEmpty();
            RuleFor(x => x.Birthdate).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}