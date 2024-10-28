using DataAccessLayer.BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public class ProductDBContext: DbContext {
    private readonly IConfiguration configuration;
    public ProductDBContext() {}
    public ProductDBContext(IConfiguration configuration) {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
    public virtual DbSet<Category> Categories {get; set;}
    public virtual DbSet<Product> Products {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category{CategoryId = 1, CategoryName = "Beverage"},
            new Category{CategoryId = 2, CategoryName = "Condiments"},
            new Category{CategoryId = 3, CategoryName = "Confection"},
            new Category{CategoryId = 4, CategoryName = "Dairy Products"},
            new Category{CategoryId = 5, CategoryName = "Grains/Cereals"},
            new Category{CategoryId = 6, CategoryName = "Meat/Poultry"},
            new Category{CategoryId = 7, CategoryName = "Produce"},
            new Category{CategoryId = 8, CategoryName = "Seafood"}
        );
    }
}
