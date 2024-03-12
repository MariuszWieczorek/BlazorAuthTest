using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Temp
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Idx01 { get; set; }
    public string? Idx02 { get; set; }

}
