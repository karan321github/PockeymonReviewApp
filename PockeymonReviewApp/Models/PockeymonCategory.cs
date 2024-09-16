namespace PockeymonReviewApp.Models
{
    public class PockeymonCategory
    {
        public int PockeymonId { get; set; }
        public int CategoryId { get; set; }
        public Pockymon Pockymon { get; set; }
        public Category Category { get; set; }
        public int OwnerId { get; internal set; }
    }
}
