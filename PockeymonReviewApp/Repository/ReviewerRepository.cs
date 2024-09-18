using Microsoft.EntityFrameworkCore;
using PockeymonReviewApp.Data;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
    private ApplicationDBContext _context;
        public ReviewerRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewer.Where(r => r.Id == reviewerId)
                .Include(e => e.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewer.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviwerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviwerId)
                .ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviews.Any(e => e.ReviewerId == reviewerId);
        }


        public bool save()
        {
          var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
