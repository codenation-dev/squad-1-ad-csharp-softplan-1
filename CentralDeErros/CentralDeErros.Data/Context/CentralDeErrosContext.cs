using CentralDeErros.Data.Config;
using CentralDeErros.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CentralDeErros.Data.Context
{
    public class CentralDeErrosContext : DbContext
    {
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

        public CentralDeErrosContext(DbContextOptions<CentralDeErrosContext> options): base(options)
        {
        }

        public CentralDeErrosContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CentralDeErros;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ErrorLogConfig());
        }
    }
}
