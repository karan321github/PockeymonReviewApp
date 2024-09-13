using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfAPockeymon(int  pokeId);

        ICollection<Pockymon> GetPockeymonByOwner(int ownerId);
        bool OwnerExist(int ownerId);

        bool CreateOwner(Owner owner);
        bool save();
    }
}
