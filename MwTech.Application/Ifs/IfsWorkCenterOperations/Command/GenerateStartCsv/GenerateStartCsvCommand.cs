using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Command.GenerateStartCsv;

public class GenerateStartCsvCommand : IRequest
{
    public string ProductNumber { get; set; }
    public decimal Qty { get; set; }
    public int OrderNumber { get; set; }
    public int OperationId { get; set; }
    public string WorkCenterNumber { get; set; }
}
