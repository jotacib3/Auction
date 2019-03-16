using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using System;

namespace Data
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<DoorsNumber> DoorsNumbers{ get; set; }
        public DbSet<EmployeeData> EmployeeDatas { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<Year> Years { get; set; }

    }
}
