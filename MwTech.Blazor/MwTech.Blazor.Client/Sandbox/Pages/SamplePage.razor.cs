using Microsoft.AspNetCore.Components;
using MwTech.Shared.Products.Dtos;

namespace MwTech.Blazor.Client.Sandbox.Pages;

public partial class SamplePage
{
    private TestBoxDto _box = new TestBoxDto
    {
        Id = 1,
        Name = "Test",
        Price = 1,
        ImageUrl = "wwwroot/Content/Images/rower-1.png"
    };

}
