namespace PockeymonReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PockeymonCategory> PockeymonCategories { get; set; }
    }
}
