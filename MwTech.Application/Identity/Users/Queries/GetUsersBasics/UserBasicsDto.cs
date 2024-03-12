namespace MwTech.Application.Identity.Users.Queries.GetUsersBasics;
public class UserBasicsDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}
