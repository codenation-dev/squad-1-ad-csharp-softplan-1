using CentralDeErros.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralDeErros.Data.Config
{
    public class ErrorLogConfig : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.ToTable("error_log");
            builder.HasKey(p => p.Id).HasName("id");
            builder.Property(p => p.Code).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Environment).IsRequired();
            builder.Property(p => p.Level).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Archieved).IsRequired();
            builder.Property(p => p.Origin).IsRequired().HasMaxLength(100);
        }
    }
}
