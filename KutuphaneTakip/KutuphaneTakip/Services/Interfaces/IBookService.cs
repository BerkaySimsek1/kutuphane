using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<Book> AddBook(Book book);
        Task<bool> UpdateBook(int id, Book book);
        Task<bool> DeleteBook(int id);
    }
}