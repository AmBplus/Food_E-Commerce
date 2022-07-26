using System.Security.Principal;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace F_e_commerce_EFCore;

public class FECommerceContext : IdentityDbContext
{
    public FECommerceContext(DbContextOptions<FECommerceContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<FoodType> FoodTypes { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }

}