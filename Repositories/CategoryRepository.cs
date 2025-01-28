using API_Movies.Data;
using API_Movies.Models;
using API_Movies.Repositories.IRepositories;

namespace API_Movies.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool CreateCategory(Category category)
        {
            category.CreationDate = DateTime.Now;
            _db.Category.Add(category);
            return SaveCategory();
        }

        public bool UpdateCategory(Category category)
        {
            category.CreationDate = DateTime.Now;

            var categoryExists = _db.Category.Find(category.Id);

            if(categoryExists != null)
            {
                _db.Entry(categoryExists).CurrentValues.SetValues(category);
            } else
            {
                _db.Category.Update(category);
            }
            
            return SaveCategory();
        }
       
        public bool DeleteCategory(Category category)
        {
            _db.Category.Remove(category);
            return SaveCategory();
        }

        public bool ExistsCategoryById(int CategoryId)
        {
            return _db.Category.Any(c => c.Id == CategoryId);
        }

        public bool ExistsCategoryByName(string CategoryName)
        {
            bool value = _db.Category.Any(c => c.CategoryName.ToLower().Trim() == CategoryName.ToLower().Trim());
            return value;
        }

        public ICollection<Category> GetCategories()
        {
           return _db.Category.OrderBy(c => c.CategoryName).ToList();
        }

        public Category GetCategoryById(int CategoryId)
        {
            return _db.Category.FirstOrDefault(c => c.Id == CategoryId);
        }

        public bool SaveCategory()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
