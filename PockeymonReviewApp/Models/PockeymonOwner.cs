namespace PockeymonReviewApp.Models
{
    public class PockeymonOwner
    {
        public int PockeymonId { get; set; }
        public int OwnerId { get; set; }
        public Pockymon Pockymon { get; set; }
        public Owner Owner { get; set; }
    }
}
