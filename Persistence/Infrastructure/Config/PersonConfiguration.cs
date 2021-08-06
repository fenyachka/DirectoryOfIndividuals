using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Infrastructure.Config
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.FirstNameGeo).IsRequired();
            builder.Property(p => p.FirstNameEn).IsRequired();
            builder.Property(p => p.LastNameGeo).IsRequired();
            builder.Property(p => p.LastNameEn).IsRequired();
            builder.Property(p => p.PrivateNumber).IsRequired();
            builder.Property(p => p.Birthdate).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.Phone).IsRequired();
            builder.Property(p => p.Email).IsRequired();
        }
    }
}