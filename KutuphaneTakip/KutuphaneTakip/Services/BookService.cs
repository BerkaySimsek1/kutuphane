using KutuphaneTakip.Data;  // DbContext sınıfının bulunduğu namespace'i eklemelisiniz.
using KutuphaneTakip.Models;  // Model sınıflarının bulunduğu namespace'i eklemelisiniz.
using KutuphaneTakip.Services.Interfaces;  // Service arayüzlerinin bulunduğu namespace'i eklemelisiniz.
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books
                .Include(b => b.Author)    
                .Include(b => b.Category) 
                .ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books
                .Include(b => b.Author)    // Author ilişkisini dahil etmek için Include kullanıldı
                .Include(b => b.Category)  // Category ilişkisini dahil etmek için Include kullanıldı
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> UpdateBook(int id, Book book)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.AuthorId = book.AuthorId;
            existingBook.CategoryId = book.CategoryId;
            existingBook.Metadata = book.Metadata;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AuthorExists(int authorId)
        {
            return await _context.Authors.AnyAsync(a => a.Id == authorId);
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }
    }
}
