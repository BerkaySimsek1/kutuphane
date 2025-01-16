using KutuphaneTakip.Data;
using KutuphaneTakip.Models;
using KutuphaneTakip.Repositories.Interfaces;
using System.Threading.Tasks;

namespace KutuphaneTakip.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
            Books = new GenericRepository<Book>(_context);
            Authors = new GenericRepository<Author>(_context);
            Categories = new GenericRepository<Category>(_context);
            Members = new GenericRepository<Member>(_context);
            Loans = new GenericRepository<Loan>(_context);
        }

        public IRepository<Book> Books { get; private set; }
        public IRepository<Author> Authors { get; private set; }
        public IRepository<Category> Categories { get; private set; }
        public IRepository<Member> Members { get; private set; }
        public IRepository<Loan> Loans { get; private set; }

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