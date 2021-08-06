using System;
using System.Threading.Tasks;

namespace Domain.Intefaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
        IRepository<Person> Person { get; }
        IRepository<RelatedPeople> RelatedPeople { get; }
    }
}