using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;


public class IfsRoute
{


    public Guid Id { get; set; }
    public string? Contract { get; set; }
    public string? PartNo { get; set; }
    public string? AlternativeNo { get; set; }
    public string? RevisionNo { get; set; }

    //

    public string? OperationNo { get; set; }
    public string? OperationDescription { get; set; }
    public string? AlternativeDescription { get; set; }

    //

    public string? WorkCenterNo { get; set; }
    public string? LaborClassNo { get; set; }
    public decimal MachRunFactor { get; set; }
    public decimal LaborRunFactor { get; set; }
    public decimal CrewSize { get; set; }
    public string? RunTimeCode { get; set; }

    //

    public string? SetupLaborClassNo { get; set; }
    public decimal? MachSetupTime { get; set; }
    public decimal? LaborSetupTime { get; set; }
    public decimal? SetupCrewSize { get; set; }

    //

    public decimal? MoveTime { get; set; }
    public decimal? Overlap { get; set; }

    //
    public int? OperationId { get; set; }
    //
    public string? ToolId { get; set; }
    public int? ToolQuantity { get; set; }

}
