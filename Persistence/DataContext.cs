using System.Reflection;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<RelatedPeople> RelatedPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RelatedPeople>()
                .HasOne(p => p.RelatedPerson)
                .WithMany()
                .HasForeignKey(p => p.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelatedPeople>()
                .HasOne(x => x.Person)
                .WithMany(y => y.RelatedPeople)
                .HasForeignKey(pt => pt.PersonId);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}