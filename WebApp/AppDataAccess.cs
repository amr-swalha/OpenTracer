using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp;

public class AppDataAccess : DbContext
{
    public AppDataAccess(DbContextOptions<AppDataAccess> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Traces>().ToTable("traces", "main").HasKey(e => e.Id);
        builder.Entity<Traces>().Property(y => y.Id).HasColumnName(nameof(Traces.Id).ToLower());
        builder.Entity<Traces>().Property(y => y.CreationDate).HasColumnName(nameof(Traces.CreationDate).ToLower());
        builder.Entity<Traces>().Property(y => y.Details).HasColumnName(nameof(Traces.Details).ToLower());
        builder.Entity<Traces>().Property(y => y.Details).HasColumnType("jsonb");
        builder.Entity<Traces>().Property(y => y.Id).HasDefaultValueSql("newsequentialid()");
    }
}