using API_Movies.Models;

namespace API_Movies.Repositories.IRepositories
{
    public interface ICategory
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int CategoryId);
        bool ExistsCategoryById(int CategoryId);
        bool ExistsCategoryByName(string CategoryName);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool SaveCategory();

    }
}
