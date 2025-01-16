using KutuphaneTakip.Repositories.Interfaces;
using KutuphaneTakip.Models;
using KutuphaneTakip.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _unitOfWork.Categories.GetAll();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _unitOfWork.Categories.GetById(id);
        }

        public async Task<Category> AddCategory(Category category)
        {
            await _unitOfWork.Categories.Add(category);
            await _unitOfWork.Complete();
            return category;
        }

        public async Task<bool> UpdateCategory(int id, Category category)
        {
            var existingCategory = await _unitOfWork.Categories.GetById(id);
            if (existingCategory == null)
            {
                return false;
            }

            existingCategory.Name = category.Name;

            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Categories.GetById(id);
            if (category == null)
            {
                return false;
            }

            await _unitOfWork.Categories.Delete(category);
            await _unitOfWork.Complete();
            return true;
        }
    }
}