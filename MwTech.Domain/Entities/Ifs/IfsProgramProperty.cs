using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;

public class IfsProductProperty
{
    public string Property { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? Value { get; set; }
    public decimal? MaxValue { get; set; }
    public string Text { get; set; }
    public string Unit { get; set; }
}
