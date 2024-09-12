namespace PockeymonReviewApp.Models
{
    public class Pockymon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PockeymonOwner> PockeymonOwners { get; set; }
        public ICollection<PockeymonCategory> PockeymonCategories { get; set; }
    }
}
