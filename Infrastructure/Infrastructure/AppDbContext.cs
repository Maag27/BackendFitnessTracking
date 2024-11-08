using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;


namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Milk> Milks { get; set; }
        public DbSet<Cow> Cows { get; set; }
        public DbSet<UserMetrics> UserMetrics { get; set; }

    }

}
