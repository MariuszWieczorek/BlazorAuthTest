using Microsoft.AspNetCore.Components;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Services;
using MwTech.Shared.Tyres.Tyres.Commands.AddTyre;
using Newtonsoft.Json;

namespace MwTech.Blazor.Client.Pages.Tyres;

public partial class TyreAdd
{


    [Inject]
    public ToastrService ToastrService { get; set; }

    [Inject]
    public ITyreHttpRepository TyresHttpRepo { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private AddTyreCommand _tyre = new();
    private bool _isLoading = false;



    private async Task Save()
    {

        try
        {
            _isLoading = true;

            // await Task.Delay(2000);

            var json = JsonConvert.SerializeObject(_tyre);
            

            await TyresHttpRepo.Add(_tyre);
            //logika zapisu do bazy danych
            
            await ToastrService.ShowInfoMessage($"Dane zostały zapisane.\n {json}.");
            NavigationManager.NavigateTo("tyres");
        }
        finally
        {
            _isLoading = false;
        }
    }

}
