using System.Threading.Tasks;
using Domain;
using Domain.Intefaces;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IRepository<Person> Person { get; }

        public IRepository<RelatedPeople> RelatedPeople { get; }

        public UnitOfWork(DataContext context, IRepository<Person> person, IRepository<RelatedPeople> relatedPeople)
        {
            _context = context;
            Person = person;
            RelatedPeople = relatedPeople;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}