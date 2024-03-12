using Microsoft.AspNetCore.Components;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Services;
using MwTech.Shared.Currencies.Commands.AddCurrency;
using MwTech.Shared.Currencies.Commands.EditCurrency;
using Newtonsoft.Json;

namespace MwTech.Blazor.Client.Pages.Currencies;

public partial class CurrencyEdit
{
    [Parameter]   
    public int Id { get; set; }

    [Inject]
    public ToastrService ToastrService { get; set; }

    [Inject]
    public ICurrencyHttpRepository CurrencyHttpRepo { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private EditCurrencyCommand _currency = new();
    private bool _isLoading = false;

    protected override async Task OnInitializedAsync()
    {
        _currency  = await CurrencyHttpRepo.GetEdit(Id);
       
    }

    private async Task Save()
    {

        try
        {
            _isLoading = true;

            // await Task.Delay(2000);

            var json = JsonConvert.SerializeObject(_currency);
            

            await CurrencyHttpRepo.Edit(_currency);
            //logika zapisu do bazy danych
            
            await ToastrService.ShowInfoMessage($"Dane zostały zapisane.\n {json}.");
            NavigationManager.NavigateTo("currencies");
        }
        finally
        {
            _isLoading = false;
        }
    }

}
