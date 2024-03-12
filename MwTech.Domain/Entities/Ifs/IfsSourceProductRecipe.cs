using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;


public class IfsSourceProductRecipe
{
    public string? CONTRACT { get; set; }
    public string PART_NO { get; set; }
    public string ALTERNATIVE_NO { get; set; }
    public string REVISION_NO { get; set; }
    public int LINE_ITEM_NO { get; set; }
    public int LINE_SEQUENCE { get; set; }
    //
    public string? REVISION_NAME { get; set; }
    public string? ALTERNATIVE_DESCRIPTION { get; set; }
    //
    public string COMPONENT_PART { get; set; }
    public decimal QTY_PER_ASSEMBLY { get; set; }
    public decimal PARTS_BY_WEIGHT { get; set; }
    public decimal? SHRINKAGE_FACTOR { get; set; }
    public decimal? COMPONENT_SCRAP { get; set; }
    public string? CONSUMPTION_ITEM_DB { get; set; }
    //
    public string? PRINT_UNIT { get; set; }
    public DateTime? EFF_PHASE_IN_DATE { get; set; }
    public DateTime? EFF_PHASE_OUT_DATE { get; set; }
    public string? ALTERNATIVE_STATE { get; set; }
    public string? PART_STATUS { get; set; }

}
