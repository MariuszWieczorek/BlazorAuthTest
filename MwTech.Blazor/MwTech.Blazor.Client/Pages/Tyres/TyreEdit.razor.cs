using Microsoft.AspNetCore.Components;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Services;
using MwTech.Shared.Tyres.Tyres.Commands.EditTyre;
using Newtonsoft.Json;

namespace MwTech.Blazor.Client.Pages.Tyres;

public partial class TyreEdit
{

    [Parameter]
    public int Id { get; set; }

    [Inject]
    public ToastrService ToastrService { get; set; }

    [Inject]
    public ITyreHttpRepository TyresHttpRepo { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private EditTyreCommand _tyre = new();
    private bool _isLoading = false;


    protected override async Task OnInitializedAsync()
    {
        _tyre = await TyresHttpRepo.GetEdit(Id);

    }

    private async Task Save()
    {

        try
        {
            _isLoading = true;

            // await Task.Delay(2000);

            var json = JsonConvert.SerializeObject(_tyre);
            

            await TyresHttpRepo.Edit(_tyre);
            //logika zapisu do bazy danych
            
            await ToastrService.ShowInfoMessage($"Dane zostały zapisane.");
            NavigationManager.NavigateTo("tyres");
        }
        finally
        {
            _isLoading = false;
        }
    }

}
