using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace HouseRental.Models;

public class HouseDbContext : IdentityDbContext
{
    public HouseDbContext(DbContextOptions<HouseDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<House> Houses { get; set; }


    public DbSet<Owner> Owners { get; set; }
    public DbSet<Renter> Renters { get; set; }
    public DbSet<LeaseAgreement> LeaseAgreements { get; set; }

    
    //Lazy loading
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
   
}
