using Microsoft.EntityFrameworkCore;

namespace WhatsForDinner.Models{
    public class DinnerDbContext : DbContext{
        public DinnerDbContext(DbContextOptions<DinnerDbContext> options) : base(options) { }

        public DbSet<Ingredient> Ingredients => Set<Ingredient>(); //creating the Ingredients table

        public DbSet<Category> categories=> Set<Category>(); //creating the Categories table
    }
}