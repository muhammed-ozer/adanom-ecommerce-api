using Adanom.Ecommerce.API.Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Logging
{
    public sealed class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AdminTransactionLog> AdminTransactionLogs { get; set; }

        public DbSet<AuthLog> AuthLogs { get; set; }

        public DbSet<CustomerTransactionLog> CustomerTransactionLogs { get; set; }
    }
}
