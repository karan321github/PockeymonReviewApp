using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();

        public Category GetCategory(int id);
        bool isCategoryExist(int id);

        ICollection<Pockymon> GetPockeymonByCategory(int categoryId);

        public bool CreateCategory(Category category);

        bool save();

    }
}
