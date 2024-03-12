using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MwTech.Blazor.Client.Sandbox.Pages;

public partial class Counter
{
    [Inject]
    public ICurrencyHttpRepository CurrencyHttpRepo { get; set; }

    private int currentCount = 0;

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public ToastrService ToastrService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var counter = await LocalStorage.GetItemAsync<int>("counter");
            currentCount = counter;
            StateHasChanged();
        }
    }

    private async Task IncrementCount()
    {
        currentCount++;
        await LocalStorage.SetItemAsync("counter", currentCount);
        await ToastrService.ShowInfoMessage($"Licznik zwiększony do -> {currentCount}");
        //throw new Exception("dziwny błąd");

    }
}
