using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Operation
{
    public int Id { get; set; }
    public string OperationNumber { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    public int ProductCategoryId { get; set; }
    public Unit? Unit { get; set; }
    public int UnitId { get; set; }
    public int No { get; set; }
    public ICollection<ManufactoringRoute> ManufactoringRoutes { get; set; } = new HashSet<ManufactoringRoute>();

}
