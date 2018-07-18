using EFCoreDemos.GroupBy.ComplexOne;
using EFCoreDemos.GroupBy.ComplexTwo;
using EFCoreDemos.GroupBy.Simple;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace EFCoreDemos
{
  public class GroupByTests
  {
    [Fact]
    public void Simple_GroupByCategory()
    {
      var options = new DbContextOptionsBuilder<SimpleDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

      // Run the test against one instance of the context
      using (var dc = new SimpleDbContext(options))
      {
        var entities = new[] {
          new SimpleEntity{ Name = "Bir", Category = "Bir" },
          new SimpleEntity{ Name = "İki", Category = "Bir" },
          new SimpleEntity{ Name = "Üç", Category = "Bir" },
          new SimpleEntity{ Name = "Dört", Category = "Bir" },
          new SimpleEntity{ Name = "Beş", Category = "Bir" },
          new SimpleEntity{ Name = "On Bir", Category = "İki" },
          new SimpleEntity{ Name = "On İki", Category = "İki" },
          new SimpleEntity{ Name = "On Üç", Category = "İki" },
          new SimpleEntity{ Name = "On Dört", Category = "İki" },
          new SimpleEntity{ Name = "On Beş", Category = "İki" }
        };
        dc.Simples.AddRange(entities);
        dc.SaveChanges();

        var gp = dc.Simples.GroupBy(x => x.Category).ToList();
        gp.ShouldNotBeNull();
        gp.Count.ShouldBeGreaterThan(0);
      }
    }

    
    [Fact]
    public void ComplexOne_GroupBySizeIntType()
    {
      using (var dc = CreateComplexOneTestDatas())
      {
        var gp = dc.Products.GroupBy(x => x.Size).ToList();
        gp.ShouldNotBeNull();
        gp.Count.ShouldBeGreaterThan(0);
      }
    }

    [Fact]
    public void ComplexOne_GroupBySexEnumType()
    {
      using (var dc = CreateComplexOneTestDatas())
      {
        var gp = dc.Products.GroupBy(x => x.Gender).ToList();
        gp.ShouldNotBeNull();
        gp.Count.ShouldBeGreaterThan(0);
      }
    }

    [Fact]
    public void ComplexOne_GroupByCategoryName()
    {
      using (var dc = CreateComplexOneTestDatas())
      {
        var gp = dc.Products.GroupBy(x => x.Category.Name).ToList();
        gp.ShouldNotBeNull();
        gp.Count.ShouldBeGreaterThan(0);
      }
    }

    [Fact]
    public void ComplexTwo_GroupByWithIntType()
    {
      using (var dc = CreateComplexTwoTestDatas())
      {
        var gp = dc.Lines.GroupBy(x => x.Width).ToList();
        gp.ShouldNotBeNull();
        gp.Count.ShouldBeGreaterThan(0);
      }
    }

    [Fact]
    public void ComplexTwo_GroupByDrawTypeEnumType()
    {
      using (var dc = CreateComplexTwoTestDatas())
      {
        var gp = dc.Lines.GroupBy(x => x.DrawType).ToList();
        gp.ShouldNotBeNull();
        gp.Count.ShouldBeGreaterThan(0);
      }
    }

    [Fact]
    public void ComplexTwo_GroupByStartPointName()
    {
      using (var dc = CreateComplexTwoTestDatas())
      {
        var gp = dc.Lines.GroupBy(x => x.StartPoint.Name).ToList();
        gp.ShouldNotBeNull();
        gp.Count.ShouldBeGreaterThan(0);
      }
    }

    [Fact]
    public void ComplexTwo_GroupByEndPointName()
    {
      using (var dc = CreateComplexTwoTestDatas())
      {
        var gp = dc.Lines.GroupBy(x => x.EndPoint.Name).ToList();
        gp.ShouldNotBeNull();
        gp.Count.ShouldBeGreaterThan(0);
      }
    }

    private static ComplexOneDbContext CreateComplexOneTestDatas()
    {
      var options = new DbContextOptionsBuilder<ComplexOneDbContext>()
                      .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                      .Options;

      // Run the test against one instance of the context
      var dc = new ComplexOneDbContext(options);
      var category1 = new CategoryEntity() { Name = "Food" };
      var category2 = new CategoryEntity() { Name = "Clothing" };

      dc.Categories.Add(category1);
      dc.Categories.Add(category2);
      dc.Products.Add(new ProductEntity() { Category = category1, Name = "Pizza", Gender = GenderEnum.None, Size = 3 });
      dc.Products.Add(new ProductEntity() { Category = category1, Name = "Suchi", Gender = GenderEnum.None, Size = 2 });
      dc.Products.Add(new ProductEntity() { Category = category2, Name = "Trousers", Gender = GenderEnum.Male, Size = 3 });
      dc.Products.Add(new ProductEntity() { Category = category2, Name = "Man T-Shirt", Gender = GenderEnum.Male, Size = 3 });
      dc.Products.Add(new ProductEntity() { Category = category2, Name = "Woman T-Shirt", Gender = GenderEnum.Female, Size = 3 });
      dc.Products.Add(new ProductEntity() { Category = category2, Name = "Skirt", Gender = GenderEnum.Female, Size = 2 });

      dc.SaveChanges();

      return dc;
    }





    private static ComplexTwoDbContext CreateComplexTwoTestDatas()
    {
      var options = new DbContextOptionsBuilder<ComplexTwoDbContext>()
                      .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                      .Options;

      // Run the test against one instance of the context
      var dc = new ComplexTwoDbContext(options);
      var points = new[] {
        new PointEntity() { Name = "Origin", X = 0.0, Y = 0.0 },
        new PointEntity() { Name = "First", X = 1.0, Y = 1.0 },
        new PointEntity() { Name = "Second", X = 2.0, Y = 2.0 },
        new PointEntity() { Name = "Third", X = 3.0, Y = 2.0 },
        new PointEntity() { Name = "Fourth", X = 2.0, Y = 3.0 },
      };

      dc.AddRange(points);

      var lines = new[] {
        new LineEntity() { Name = "First Line", StartPoint = points[0], EndPoint = points[1], DrawType = DrawTypeEnum.Solid, Width = 3},
        new LineEntity() { Name = "Second Line", StartPoint = points[1], EndPoint = points[2], DrawType = DrawTypeEnum.Solid, Width = 2},
        new LineEntity() { Name = "Third Line", StartPoint = points[2], EndPoint = points[3], DrawType = DrawTypeEnum.Solid, Width = 2},
        new LineEntity() { Name = "Fourth Line", StartPoint = points[2], EndPoint = points[4], DrawType = DrawTypeEnum.Solid, Width = 3},
        new LineEntity() { Name = "Fifth Line", StartPoint = points[1], EndPoint = points[3], DrawType = DrawTypeEnum.Solid, Width = 3},
        new LineEntity() { Name = "Sixth Line", StartPoint = points[1], EndPoint = points[4], DrawType = DrawTypeEnum.Solid, Width = 2},
      };

      dc.AddRange(lines);

      dc.SaveChanges();

      return dc;
    }
  }
}
