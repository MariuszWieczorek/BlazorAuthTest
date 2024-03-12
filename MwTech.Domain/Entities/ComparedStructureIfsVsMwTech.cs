using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ComparedStructureIfsVsMwTech
{
    
    public string PartNo { get; set; }
    public string AlternativeNo { get; set; }
    public string RevisionNo { get; set; }
    public int LineSequence { get; set; }
    //
    public string? IfsComponentPart { get; set; }
    public string? MwComponentPart { get; set; }
    public decimal? IfsQtyPerAssembly { get; set; }
    public decimal? MwQtyPerAssembly { get; set; }
    public decimal? IfsShrinkageFactor { get; set; }
    public decimal? MwShrinkageFactor { get; set; }
    public string? IfsConsumptionItem { get; set; }
    public string? MwConsumptionItem { get; set; }
    //
    public int ProductId { get; set; }
    public string? CategoryNumber { get; set; }
    public bool? IsProductActive { get; set; }
    //
    public int TestShrinkageFactor { get; set; }
    public int TestQtyPerAssembly { get; set; }
    public int TestComponentPart { get; set; }
    public int TestExistAlternativeNo { get; set; }
    public int TestExistsRevisionNo { get; set; }
    public int TestProductExists { get; set; }
    public int TestRevAndAltExists { get; set; }

}
