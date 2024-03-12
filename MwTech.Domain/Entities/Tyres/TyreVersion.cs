namespace MwTech.Domain.Entities.Tyres;

public class TyreVersion
{
    public int Id { get; set; }

    public string Name { get; set; }
    public int TyreId { get; set; }
    public Tyre Tyre { get; set; }


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

}
