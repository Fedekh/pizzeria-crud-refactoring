using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pizzeria_crud_refactoring.Models;

namespace pizzeria_mvc.Database
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }

        //private string sqlString = "Server=GAMMA;Database=Pizza;TrustServerCertificate=True";
        private string sqlString = "Server=DESKTOP-1DGOME6;Database=Pizza;Trusted_Connection=True;TrustServerCertificate=True";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(sqlString);
    }
}