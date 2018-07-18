using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace EFCoreDemos.GroupBy.Simple
{
  public class SimpleDbContext : DbContext
  {
    public SimpleDbContext()
    { }

    public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
        : base(options)
    { }

    public DbSet<SimpleEntity> Simples { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
    }
  }
}
