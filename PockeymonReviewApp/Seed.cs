using PockeymonReviewApp.Data;
using PockeymonReviewApp.Models;


namespace PokemonReviewApp
{
    public class Seed
    {
        private readonly ApplicationDBContext dataContext;
        public Seed(ApplicationDBContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.PockeymonOwners.Any())
            {
                // Step 1: Create Categories
                var categories = new List<Category>
        {
            new Category { Name = "Electric" },
            new Category { Name = "Water" },
            new Category { Name = "Leaf" }
        };
                dataContext.Categories.AddRange(categories);
                dataContext.SaveChanges();

                // Step 2: Create Countries
                var countries = new List<Country>
        {
            new Country { Name = "Kanto" },
            new Country { Name = "Saffron City" },
            new Country { Name = "Millet Town" }
        };
                dataContext.Countries.AddRange(countries);
                dataContext.SaveChanges();

                // Step 3: Create Pockeymon
                var pockeymons = new List<Pockymon>
        {
            new Pockymon
            {
                Name = "Pikachu",
                DateOfBirth = new DateTime(1903,1,1),
                Description = "High voltage super power pockymon"
            },
            new Pockymon
            {
                Name = "Squirtle",
                DateOfBirth = new DateTime(1903,1,1),
                Description = "Water-type starter Pokémon"
            },
            new Pockymon
            {
                Name = "Venasuar",
                DateOfBirth = new DateTime(1903,1,1),
                Description = "Fully evolved form of Bulbasaur"
            }
        };
                dataContext.Pockeymon.AddRange(pockeymons);
                dataContext.SaveChanges();

                // Step 4: Create PockeymonCategories
                var pockeymonCategories = new List<PockeymonCategory>();
                for (int i = 0; i < pockeymons.Count; i++)
                {
                    pockeymonCategories.Add(new PockeymonCategory
                    {
                        PockeymonId = pockeymons[i].Id,
                        CategoryId = categories[i].Id
                    });
                }
                dataContext.PockeymonCategories.AddRange(pockeymonCategories);
                dataContext.SaveChanges();

                // Step 5: Create Owners
                var owners = new List<Owner>
        {
            new Owner { FirstName = "Jack", LastName = "London", Gym = "Brocks Gym", CountryId = countries[0].Id },
            new Owner { FirstName = "Harry", LastName = "Potter", Gym = "Mistys Gym", CountryId = countries[1].Id },
            new Owner { FirstName = "Ash", LastName = "Ketchum", Gym = "Ashs Gym", CountryId = countries[2].Id }
        };
                dataContext.Owner.AddRange(owners);
                dataContext.SaveChanges();

                // Step 6: Create PockeymonOwners
                var pockeymonOwners = new List<PockeymonOwner>();
                for (int i = 0; i < pockeymons.Count; i++)
                {
                    pockeymonOwners.Add(new PockeymonOwner
                    {
                        PockeymonId = pockeymons[i].Id,
                        OwnerId = owners[i].Id
                    });
                }
                dataContext.PockeymonOwners.AddRange(pockeymonOwners);
                dataContext.SaveChanges();

                // Step 7: Create Reviewers
                var reviewers = new List<Reviewer>
        {
            new Reviewer { FirstName = "Teddy", LastName = "Smith" },
            new Reviewer { FirstName = "Taylor", LastName = "Jones" },
            new Reviewer { FirstName = "Jessica", LastName = "McGregor" }
        };
                dataContext.Reviewer.AddRange(reviewers);
                dataContext.SaveChanges();

                // Step 8: Create Reviews
                var reviews = new List<Review>();
                string[] titles = { "Pikachu", "Squirtle", "Veasaur" };
                for (int i = 0; i < pockeymons.Count; i++)
                {
                    for (int j = 0; j < reviewers.Count; j++)
                    {
                        reviews.Add(new Review
                        {
                            Title = titles[i],
                            Text = $"{titles[i]} review {j + 1}",
                            Rating = 5 - (j * 2),
                            ReviewerId = reviewers[j].Id,
                            PockeymonId = pockeymons[i].Id
                        });
                    }
                }
                dataContext.Reviews.AddRange(reviews);
                dataContext.SaveChanges();
            }
        }
    }
}