using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDemos.GroupBy.ComplexTwo
{
  public class ComplexTwoDbContext : DbContext
  {
    public ComplexTwoDbContext() : base()
    {

    }
    public ComplexTwoDbContext(DbContextOptions<ComplexTwoDbContext> options)
        : base(options)
    { }


    public DbSet<PointEntity> Points { get; set; }
    public DbSet<LineEntity> Lines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<LineEntity>()
        .HasOne(x => x.StartPoint)
        .WithMany()
        .HasForeignKey(x => x.StartPointId)
        .IsRequired();

      modelBuilder.Entity<LineEntity>()
        .HasOne(x => x.EndPoint)
        .WithMany()
        .HasForeignKey(x => x.EndPointId)
        .IsRequired();

      modelBuilder.Entity<LineEntity>()
        .Property(m => m.DrawType)
        .HasConversion(
            v => v.ToString(),
            v => (DrawTypeEnum)Enum.Parse(typeof(DrawTypeEnum), v));
    }
  }
}
