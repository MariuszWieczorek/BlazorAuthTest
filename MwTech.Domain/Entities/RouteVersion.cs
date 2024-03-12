using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class RouteVersion
{
    public int Id { get; set; }
    public int VersionNumber { get; set; }
    public int AlternativeNo { get; set; }
    public bool DefaultVersion { get; set; }
    public bool ComarchDefaultVersion { get; set; }
    public bool IfsDefaultVersion { get; set; }
    public bool ToIfs { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = String.Empty;
    public Product? Product { get; set; }
    public int ProductId { get; set; }
    public decimal ProductQty { get; set; } = 1;
    public string? Description { get; set; }

    // akceptacja level 1
    public bool IsAccepted01 { get; set; }
    public string? Accepted01ByUserId { get; set; }
    public ApplicationUser? Accepted01ByUser { get; set; }
    public DateTime? Accepted01Date { get; set; }

    // akceptacja level 2
    public bool IsAccepted02 { get; set; }
    public string? Accepted02ByUserId { get; set; }
    public ApplicationUser? Accepted02ByUser { get; set; }
    public DateTime? Accepted02Date { get; set; }

    // utworzenie wersji
    public DateTime? CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }

    // modyfikacja wersji
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }


    //
    public int? ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }

    public ICollection<ManufactoringRoute> ManufactoringRoutes { get; set; } = new HashSet<ManufactoringRoute>();

}
