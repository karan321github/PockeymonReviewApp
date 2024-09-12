using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Interfaces
{
    public interface IPockeymonRepository
    {
        ICollection<Pockymon> GetPockeymons();
        Pockymon GetPockymon(int id);
        Pockymon GetPockymon(String Name);
        decimal GetPockymonRateing(int pokeId);
        bool PokemonExists(int pokeId);
        
    }
}
