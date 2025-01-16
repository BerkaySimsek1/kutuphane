using System;
using System.Threading.Tasks;
using KutuphaneTakip.Models;

namespace KutuphaneTakip.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Author> Authors { get; }
        IRepository<Category> Categories { get; }
        IRepository<Member> Members { get; }
        IRepository<Loan> Loans { get; }
        Task<int> Complete();
    }
}