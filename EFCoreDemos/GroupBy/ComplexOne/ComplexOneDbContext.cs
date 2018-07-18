using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDemos.GroupBy.ComplexOne
{
  public class ComplexOneDbContext : DbContext
  {
    public ComplexOneDbContext() : base()
    {

    }
    public ComplexOneDbContext(DbContextOptions<ComplexOneDbContext> options)
        : base(options)
    { }


    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ProductEntity>()
        .HasOne(x => x.Category)
        .WithMany()
        .HasForeignKey(x => x.CategoryId)
        .IsRequired();

      modelBuilder.Entity<ProductEntity>()
        .Property(m=>m.Gender)
        .HasConversion(
            v => v.ToString(),
            v => (GenderEnum)Enum.Parse(typeof(GenderEnum), v));
    }
  }
}
