using PockeymonReviewApp.Data;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context) 
        {
            _context = context;
        }
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool isCategoryExist(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        public ICollection<Pockymon> GetPockeymonByCategory(int categoryId)
        {
            return _context.PockeymonCategories.Where(e => e.CategoryId == categoryId).Select(e => e.Pockymon).ToList();
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return save();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
