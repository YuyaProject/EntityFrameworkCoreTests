using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDemos.GroupBy.ComplexOne
{

  public class ProductEntity
  {
    public int Id { get; set; }
    public CategoryEntity Category { get; set; }
    public int CategoryId { get; set; }

    public string Name { get; set; }
    public GenderEnum Gender { get; set; } 
    public int Size { get; set; }
  }
}
