using KutuphaneTakip.Repositories.Interfaces;
using KutuphaneTakip.Models;
using KutuphaneTakip.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _unitOfWork.Authors.GetAll();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _unitOfWork.Authors.GetById(id);
        }

        public async Task<Author> AddAuthor(Author author)
        {
            await _unitOfWork.Authors.Add(author);
            await _unitOfWork.Complete();
            return author;
        }

        public async Task<bool> UpdateAuthor(int id, Author author)
        {
            var existingAuthor = await _unitOfWork.Authors.GetById(id);
            if (existingAuthor == null)
            {
                return false;
            }

            existingAuthor.Name = author.Name;
            existingAuthor.Surname = author.Surname;

            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _unitOfWork.Authors.GetById(id);
            if (author == null)
            {
                return false;
            }

            await _unitOfWork.Authors.Delete(author);
            await _unitOfWork.Complete();
            return true;
        }
    }
}