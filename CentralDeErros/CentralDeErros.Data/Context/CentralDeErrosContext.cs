using CentralDeErros.Data.Config;
using CentralDeErros.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Data.Context
{
    public class CentralDeErrosContext : DbContext
    {
        public CentralDeErrosContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ErrorLogConfig());
        }
    }
}
