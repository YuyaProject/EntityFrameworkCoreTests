using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDemos.GroupBy.ComplexTwo
{
  public class LineEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public int StartPointId { get; set; }
    public PointEntity StartPoint { get; set; }

    public int EndPointId { get; set; }
    public PointEntity EndPoint { get; set; }
     
    public DrawTypeEnum DrawType { get; set; }
    public int Width { get; set; }
  }
}
