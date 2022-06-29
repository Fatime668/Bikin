using Bikin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bikin.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>()
                .HasIndex(u => u.Key)
                .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Logo> Logos { get; set; }
    }
}
