using FirstCrudProject.Models.DBEntities; // Importing the namespace where the DB entity models are defined
using Microsoft.EntityFrameworkCore; // Importing the Entity Framework Core namespace for database functionalities

namespace FirstCrudProject.DAL // Defining a namespace for the Data Access Layer (DAL)
{
    // Defining the ApplicationDbContext class which inherits from DbContext
    public class ApplicationDbContext : DbContext
    {
        // Constructor for ApplicationDbContext that accepts DbContextOptions and passes it to the base DbContext constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            // Constructor body - currently empty
        }

        // Defining a DbSet property for Employee entities, which represents the Employees table in the database
        public DbSet<Employee> Employees { get; set; }

        // Defining a DbSet property for Employee entities, which represents the Employees table in the database
        public DbSet<FirstCrudProject.Models.DBEntities.Product>? Product { get; set; }
    }
}
