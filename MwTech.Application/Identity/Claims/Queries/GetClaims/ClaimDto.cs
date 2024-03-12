namespace MwTech.Application.Identity.Claims.Queries.GetClaims;
public class ClaimDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string[] IdsToAdd { get; set; }
    public string[] IdsToDelete { get; set; }
}
