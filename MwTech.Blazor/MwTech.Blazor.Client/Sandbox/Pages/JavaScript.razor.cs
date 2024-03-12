using MwTech.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace MwTech.Blazor.Client.Sandbox.Pages;

public partial class JavaScript
{
    private bool _dialogResult = false;
    private string _currentDate = "";
    private IJSObjectReference _jsModule;

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public ToastrService ToastrService { get; set; }



    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/jsTestModule.js");
        }

    }


    // funkcja js nie zwracająca danych
    // podajemy funkcję js, którą chcemy wywołać, następnie jej parametry 
    private async Task DisplayAlert()
    {
        await JSRuntime.InvokeVoidAsync("alert", "Przykładowa wiadomośc");
    }

    // funkcja JS ma zwrócić informację jaki przycisk został kliknięty
    // wywołujemy generyczną wersję metody InvokeAsync
    // podajemy funkcję js, którą chcemy wywołać, następnie jej parametry 

    private async Task DisplayConfirmDialog()
    {
        _dialogResult = await JSRuntime.InvokeAsync<bool>("confirm", "Czy na pewno chcesz usunąć rekord?");
    }


    // wywołuję własną funkcję js z pliku wwwroot/scripts/jsFunctions.js z projektu Server
    private async Task ShowResultJs()
    {
        await JSRuntime.InvokeVoidAsync("addNumberJS", 1, 9);
    }

    private async Task ChangeBackgroundColor()
    {
        await ToastrService.ShowInfoMessage("Zmiana koloru tła na czerwony!");
        await JSRuntime.InvokeVoidAsync("changeBackgroundColor");
    }


    #region Wywołanie funkcji C# w JavaScript

    // W Blazorze możemy utworzyć własną funkcję w C# i użyć ją w JavaScript.
    // Dodajemy nową metodę C#, co ważne musi być ona publiczna i statyczna.
    // Oznaczamy ją atrybutem [JSInvokable], aby była dostępna z poziomu JS.
    // Następnie tworzymy funkcję w JS,
    // ponownie w pliku wwwroot/scripts/jsFunctions.js w projekcie Server
    // w niej wywołujemy DotNet.invokeMethodAsync()
    // Ważne: Jako parametry przekazujemy namespace naszej aplikacji w Blazor,
    // dalej: nazwę metody, którą chcemy wywołać i kolejno argumenty

    [JSInvokable]
    public static int Add(int number1, int number2)
    {
        return number1 + number2;
    }


    [JSInvokable]
    public static string GetCurrentDate()
    {
        return DateTime.Now.ToString("dd-MM-yyyy");
    }

    #endregion


    private async Task ShowResultCSharp()
    {
        await JSRuntime.InvokeVoidAsync("addNumberCSharp", 22, 22);
    }



    private async Task ShowDate()
    {
        _currentDate = await JSRuntime.InvokeAsync<string>("GetCurrentDateCSharp");
    }


    #region JavaScript Module

    // Test modułów w Java Script
    // To drugi sposób w jaki możemy korzystać z funkcji JS w Blazorze.
    // Aby to był moduł, W pliku ze skryptami musimy słowo export przed nazwą funkcji
    // Nie musimy dodać odwołania do pliku JavaScript w pliku index.html czy też w App.razor.
    // tworzym pole prywatne typu IJSObjectReference _jsModule

    private async Task ShowResultJsModule()
    {
        await _jsModule.InvokeVoidAsync("addNumberJSModule", 1, 14);
    }

    #endregion

    private async Task ShowToastrNotification()
    {
        await ToastrService.ShowInfoMessage("Toastr wywołany w Blazor!");
    }


}
