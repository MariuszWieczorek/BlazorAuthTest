using Microsoft.AspNetCore.Components;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Services;
using MwTech.Shared.Currencies.Commands.AddCurrency;
using Newtonsoft.Json;

namespace MwTech.Blazor.Client.Pages.Currencies;

public partial class CurrencyAdd
{

    [Inject]
    public ToastrService ToastrService { get; set; }

    [Inject]
    public ICurrencyHttpRepository CurrencyHttpRepo { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private AddCurrencyCommand _currency = new();
    private bool _isLoading = false;



    private async Task Save()
    {

        try
        {
            _isLoading = true;

            // await Task.Delay(2000);

            var json = JsonConvert.SerializeObject(_currency);
            

            await CurrencyHttpRepo.Add(_currency);
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
