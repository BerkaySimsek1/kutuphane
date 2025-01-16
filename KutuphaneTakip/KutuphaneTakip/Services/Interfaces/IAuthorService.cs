using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> AddAuthor(Author author);
        Task<bool> UpdateAuthor(int id, Author author);
        Task<bool> DeleteAuthor(int id);
    }
}