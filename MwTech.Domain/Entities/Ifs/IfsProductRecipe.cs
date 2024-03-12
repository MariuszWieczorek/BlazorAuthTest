using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;


public class IfsProductRecipe
{
    public Guid Id { get; set; }
    public string? Contract { get; set; }
    public string PartNo { get; set; }
    public string AlternativeNo { get; set; }
    public string RevisionNo { get; set; }
    public int LineItemNo { get; set; }
    public int LineSequence { get; set; }
    //
    public string? RevisionName { get; set; }
    public string? AlternativeDescription { get; set; }
    //
    public string ComponentPart { get; set; }
    public decimal QtyPerAssembly { get; set; }
    public decimal PartsByWeight { get; set; }
    public decimal? ShrinkageFactor { get; set; }
    public decimal? ComponentScrap { get; set; }
    public string? ConsumptionItemDb { get; set; }
    //
    public string? PrintUnit { get; set; }
    public DateTime? EffPhaseInDate { get; set; }
    public DateTime? EffPhaseOutDate { get; set; }
    public string? AlternativeState { get; set; }
    public string? PartStatus { get; set; }


}
