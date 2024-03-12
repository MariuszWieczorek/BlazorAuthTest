using System.Reflection.Metadata.Ecma335;

namespace MwTech.Domain.Entities.Tyres;

public class Tyre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TyreNumber { get; set; }
    public string? Description { get; set; }

    public decimal RimDiameterInInches { get; set; }
    public int LoadIndex { get; set; }
    public int PlyRating { get; set; }


    // utworzenie wersji
    public DateTime? CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }


    // modyfikacja wersji
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }

    public ICollection<TyreVersion> TyreVersions { get; set; } = new HashSet<TyreVersion>();
}
