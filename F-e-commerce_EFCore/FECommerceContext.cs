using F_e_commerce_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace F_e_commerce_EFCore;

public class FECommerceContext : DbContext
{
    public FECommerceContext(DbContextOptions<FECommerceContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<FoodType> FoodTypes { get; set; }
    
}