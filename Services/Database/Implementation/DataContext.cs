using Microsoft.EntityFrameworkCore;
using Services.Database.Interfaces;
using Services.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Database.Implementation
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Developer> Developers { get; set; }
    }
}
