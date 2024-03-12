using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Commands.ImportResourcesFromExcel;
public class ResourceToImport
{
    public string ResourceNumber { get; set; }
    public string ResourceName { get; set; }
    public int ProductCategoryId { get; set; }
    public int UnitId { get; set; }
    public string Description { get; set; }

}
