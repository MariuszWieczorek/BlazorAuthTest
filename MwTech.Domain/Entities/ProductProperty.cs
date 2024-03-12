using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ProductProperty
{
    public int Id { get; set; }
    public Property? Property { get; set; }
    public int PropertyId { get; set; }
    public int ProductPropertiesVersionId { get; set; }
    public ProductPropertyVersion? ProductPropertiesVersion { get; set; }
    public decimal? Value { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public string? Text { get; set; }
}
