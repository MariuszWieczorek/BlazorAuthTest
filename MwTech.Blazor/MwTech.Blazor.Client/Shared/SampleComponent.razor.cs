using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MwTech.Shared.Products.Dtos;

namespace MwTech.Blazor.Client.Shared;

public partial class SampleComponent
{
    private string _baseUrl = string.Empty;

    [Parameter]
    public TestBoxDto TestBoxModel { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IConfiguration Configuration { get; set; }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _baseUrl = Configuration["ApiConfiguration:BaseAddress"];
    }

    private async Task OnAddBoxToCart()
    {
        var boxes = await LocalStorage.GetItemAsync<List<TestBoxDto>>("cart");

        if (boxes == null)
            boxes = new List<TestBoxDto>();

        boxes.Add(TestBoxModel);

        await LocalStorage.SetItemAsync("cart", boxes);

        NavigationManager.NavigateTo("/home");
    }
}
