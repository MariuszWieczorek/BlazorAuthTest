using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Client.Models;
using MwTech.Blazor.Client.Services.HttpInterceptor;
using MwTech.Blazor.Services;
using MwTech.Domain.Entities;
using MwTech.Shared.Currencies.Dtos;
using Radzen.Blazor;
using System.Security.Claims;


namespace MwTech.Blazor.Client.Pages.Currencies;

public partial class Currencies : IDisposable
{
    static IComponentRenderMode _renderMode = new
        InteractiveAutoRenderMode(prerender: false);

   // [Inject]
   // public IAuthorizationService AuthorizationService { get; set; }

    [Inject]
    public ICurrencyHttpRepository CurrencyHttpRepo { get; set; }

    [Inject]
    public HttpInterceptorService Interceptor { get; set; }

    [CascadingParameter]
    public Task<AuthenticationState> AuthState {  get; set; }

    [Inject]
    public ToastrService ToastrService { get; set; }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    private bool _isLoading = false;

    private PaginationInfo _paginationInfo = new();
    public int PageNumber { get; set; } = 1;

    private IEnumerable<Currency> _currencies;


    private CurrencyFilter _filter = new();


    RadzenDataGrid<Currency> grid;

    bool ExportAllPages { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Interceptor.RegisterEvent();
        await Interceptor.RegisterBeforeSendEvent();

        var filter = await LocalStorage.GetItemAsync<CurrencyFilter>("CurrencyFilter");
        if (filter != null)
        {
            _filter.Name = filter.Name;
        }

        /*
         var authState = await AuthState;
        if (authState.User.Identity.IsAuthenticated)
        {
            var x = authState.User.FindFirst(ClaimTypes.Surname)?.Value;
        }
        */

        //throw new Exception("Błąd nad błędami");

        //var scadaAccess = await AuthorizationService.AuthorizeAsync(authState.User, "ScadaAccess");
        
        await RefreshCurrencies();


    }


    private async Task RefreshCurrencies()
    {
        _isLoading = true;
        // await Task.Delay(1000);

        try
        {

            var vm  = await CurrencyHttpRepo
                .GetFiltered(_filter,PageNumber);

            if (vm != null)
            {
                _currencies = vm.Currencies2.Items;

                _paginationInfo = new PaginationInfo
                {
                    PageIndex = vm.Currencies2.PageIndex,
                    TotalCount = vm.Currencies2.TotalCount,
                    TotalPages = vm.Currencies2.TotalPages
                };
            }
        }
        finally
        {
            _isLoading = false;
        }
    }


    private async void Filter()
    {
        _isLoading = true;
        StateHasChanged();
        await LocalStorage.SetItemAsync("CurrencyFilter", _filter);
        await RefreshCurrencies();
        StateHasChanged();

    }


    private async Task OnSelectedPage(int pageNumber)
    {
        PageNumber = pageNumber;
        await RefreshCurrencies();
        StateHasChanged();
    }

    
    /*
    public void Export(string type)
    {
        service.Export("OrderDetails", type, new Query()
        {
            OrderBy = grid.Query.OrderBy,
            Filter = grid.Query.Filter,
            Select = string.Join(",", grid.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property))
                        .Select(c => c.Property.Contains(".") ? $"{c.Property} as {c.Property.Replace(".", "_")}" : c.Property))
        });
    }
    */

    private async void DeleteCurrency(int id)
    {
        await CurrencyHttpRepo.Delete(id);
        await RefreshCurrencies();
        StateHasChanged();
        await ToastrService.ShowInfoMessage($"Waluta została usunięta.");
    }


    void HandleClick(int theId)
    {
        Console.WriteLine(theId);
    }

    public void Dispose()
    {
        Interceptor.DisposeEvent();
    }
}
