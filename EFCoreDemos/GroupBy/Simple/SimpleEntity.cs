using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace EFCoreDemos.GroupBy.Simple
{
  public class SimpleEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
  }
}
