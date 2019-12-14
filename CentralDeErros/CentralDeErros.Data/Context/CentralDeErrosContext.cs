using CentralDeErros.CrossCutting.CustomTypes;
using CentralDeErros.CrossCutting.Utils;
using CentralDeErros.Data.Config;
using CentralDeErros.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CentralDeErros.Data.Context
{
    public class CentralDeErrosContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }


        public CentralDeErrosContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public CentralDeErrosContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new ErrorLogConfig());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CentralDeErrosContext).Assembly);
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Active = true,
                        CreatedAt = DateTime.UtcNow,
                        Email = "admin@centraldeerros.com",
                        Id = Guid.NewGuid(),
                        Login = "Admin",
                        Name = "Administrator",
                        Password = Utils.ToHashMD5("agesune1"),
                        UpdatedAt = DateTime.UtcNow,
                        Role = UserRoles.ADMIN
                    }
                );
        }

        internal string GetConnectionString()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (connectionString == null)
            {
                connectionString = "Server = (localdb)\\mssqllocaldb; Database = CentralDeErrosDb; Trusted_Connection = True; MultipleActiveResultSets = true";
            }
            return connectionString;
        }
    }
}
