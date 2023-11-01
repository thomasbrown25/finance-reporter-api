using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finance_reporter_api.Models;
using Microsoft.EntityFrameworkCore;

namespace finance_reporter_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // seed the skill data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<LogTrace> LogTrace { get; set; }
        public DbSet<LogException> LogException { get; set; }
        public DbSet<LogDataExchange> LogDataExchange { get; set; }
    }
}