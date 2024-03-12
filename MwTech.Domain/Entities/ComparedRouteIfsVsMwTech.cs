using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ComparedRouteIfsVsMwTech
{
    public string? CategoryNumber { get; set; }
    public int IdxNo { get; set; }
    public string? Idx { get; set; }

    // Key

    public string PartNo { get; set; }
    public string AlternativeNo { get; set; }
    public string RevisionNo { get; set; }
    public string OperationNo { get; set; }
    
    // Main
    public string? IfsAlternativeDescription { get; set; }
    public string? MwtAlternativeDescription { get; set; }
    public string? IfsOperationDescription { get; set; }
    public string? MwtOperationDescription { get; set; }
    public string? IfsWorkCenterNo { get; set; }
    public string? MwtWorkCenterNo { get; set; }
    public string? IfsLaborClassNo { get; set; }
    public string? MwtLaborClassNo { get; set; }
    public decimal? IfsLaborRunFactor { get; set; }
    public decimal? MwtLaborRunFactor { get; set; }
    public decimal? IfsMachRunFactor { get; set; }
    public decimal? MwtMachRunFactor { get; set; }
    public decimal? IfsCrewSize { get; set; }
    public decimal? MwtCrewSize { get; set; }
    public string? IfsRunTimeCode { get; set; }
    public string? MwtRunTimeCode { get; set; }

    // Setup

    public string? IfsSetupLaborClassNo { get; set; }
    public string? MwtSetupLaborClassNo { get; set; }
    public decimal? IfsLaborSetupTime { get; set; }
    public decimal? MwtLaborSetupTime { get; set; }
    public decimal? IfsMachSetupTime { get; set; }
    public decimal? MwtMachSetupTime { get; set; }
    public decimal? IfsSetupCrewSize { get; set; }
    public decimal? MwtSetupCrewSize { get; set; }
    public decimal? IfsMoveTime { get; set; }
    public decimal? MwtMoveTime { get; set; }
    public decimal? IfsOverlap { get; set; }
    public decimal? MwtOverlap { get; set; }



    public int? IfsOperationId { get; set; }

    // Tool
    public string? IfsToolId { get; set; }
    public string? MwtToolId { get; set; }
    public int? IfsToolQuantity { get; set; }
    public int? MwtToolQuantity { get; set; }

    // Other


    public int? RoutePositionId { get; set; }

    // Tests
    public int OperationDescriptionTest { get; set; }
    public int WorkCenterNoTest { get; set; }
    public int LaborClassNoTest { get; set; }
    public int CrewSizeTest { get; set; }
    public int MachRunFactorTest     { get; set; }
    public int LaborRunFactorTest { get; set; }

    public int SetupCrewSizeTest { get; set; }
    public int SetupLaborClassNoTest { get; set; }
    public int MachSetupTimeTest { get; set; }
    public int LaborSetupTimeTest { get; set; }
    public int OverlapTest { get; set; }
    public int MoveTimeTest { get; set; }
    public int ToolIdTest { get; set; }
    public int ToolQuantityTest { get; set; }
    public int TestLineExists { get; set; }

}
