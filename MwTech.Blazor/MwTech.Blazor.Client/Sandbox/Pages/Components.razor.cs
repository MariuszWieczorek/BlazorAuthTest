using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MwTech.Blazor.Client.Sandbox.Components;
using MwTech.Blazor.Client.Sandbox.Models;

namespace MwTech.Blazor.Client.Sandbox.Pages;

public partial class Components
{
    private string _info = "Komunikat z Components";
    private string _title = "Tytuł z Components";

    private string _btnText = "Więcej";
    private Card _card;
    private bool _showDialog = false;

    private Dictionary<string, object> _cardAttributes = new()
    {
        { "BtnClass", "btn btn-danger" },
        { "BtnTitle", "Więcej" },
        { "BtnDisabled", false },
        { "Style", "" },
    };

    private Dictionary<string, object> _cardBtnAttributes = new()
    {
        { "class", "btn btn-success" },
        { "title", "Więcej" },
        { "disabled", false },
        { "type", "button" },
        { "abc", "111" }
    };

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private List<CardModel> _authors = new();

    

    protected async override Task OnInitializedAsync()
    {

        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _authors = new List<CardModel>
            {
                new CardModel { Title = "Jan Kowalski", Content = "Programista C#/.NET z 20 letnim doświadczeniem.Specjalizacje: Blazor i ASP.NET Core", Image = "/files/kowalski.png", BtnText = "Więcej"},
                new CardModel { Title = "Anna Nowak", Content = "Programista C#/.NET z 10 letnim doświadczeniem.Specjalizacje: WPF", Image = "/files/nowak.png", BtnText = "Więcej"},
                new CardModel { Title = "Błażej Kwiatkowski", Content = "Programista C#/.NET z 3 letnim doświadczeniem.Specjalizacje: Frontend", Image = "/files/kwiatkowski.png", BtnText = "Więcej"}
            };
            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private void ClickMore(MouseEventArgs e)
    {
        NavigationManager.NavigateTo("/");
    }

    private void ChangeBtnText()
    {
        _btnText = "Więcej .....";
    }

    private void AddCardBorder()
    {
        _card.AddCardBorder();
    }

    private void ShowDialog()
    {
        _showDialog = true;
    }

    private void ModalAccept(MouseEventArgs e)
    {
        //logika
        Console.WriteLine("Operacja została zatwierdzona!!!!!");
        _showDialog = false;
    }

    private void ModalCancel(MouseEventArgs e)
    {
        _showDialog = false;
    }
}
