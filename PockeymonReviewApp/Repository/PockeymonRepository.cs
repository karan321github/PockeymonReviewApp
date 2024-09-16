using PockeymonReviewApp.Data;
using PockeymonReviewApp.Interfaces;
using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Repository
{
    public class PockeymonRepository : IPockeymonRepository
    {
        private readonly ApplicationDBContext _context;
        public PockeymonRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool CreatePockeymon(int ownerId, int categoryId, Pockymon pockeymon)
        {   
            var pockeymonOwnerEntity = _context.Owner.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pockeymonOwner = new PockeymonOwner()
            {
                Owner = pockeymonOwnerEntity ,
                Pockymon = pockeymon ,
            };
            _context.Add(pockeymonOwner);

            var pockeymoCategory = new PockeymonCategory()
            {
                Pockymon = pockeymon,
                Category = category,
                OwnerId = ownerId
            };

            _context.Add(pockeymoCategory);
            _context.Add(pockeymon);

            return save();
        }

        public ICollection<Pockymon> GetPockeymons()
        {
            return _context.Pockeymon.OrderBy(p => p.Id).ToList();
        }

        public Pockymon GetPockymon(int id)
        {
            return _context.Pockeymon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pockymon GetPockymon(string Name)
        {
            return _context.Pockeymon.Where(p => p.Name == Name).FirstOrDefault();
        }

        public decimal GetPockymonRateing(int pokeId)
        {
            var reviews = _context.Reviews.Where(p => p.Pockeymon.Id == pokeId);
            if (reviews.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Pockymon> GetPockymons()
        {
            return _context.Pockeymon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pockeymon.Any(p => p.Id == pokeId);

        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
