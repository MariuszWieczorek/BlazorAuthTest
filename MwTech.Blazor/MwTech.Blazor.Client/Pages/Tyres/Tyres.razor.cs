using Blazored.LocalStorage;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Client.Models;
using MwTech.Blazor.Client.Services.HttpInterceptor;
using MwTech.Blazor.Services;
using MwTech.Shared.Tyres.Tyres.Dtos;
using MwTech.Shared.Tyres.Tyres.Models;

namespace MwTech.Blazor.Client.Pages.Tyres;

public partial class Tyres : IDisposable
{
    static IComponentRenderMode _renderMode = new
        InteractiveAutoRenderMode(prerender: false);

    //[Inject]
    //public IAuthorizationService AuthorizationService { get; set; }

    [Inject]
    public ITyreHttpRepository TyreHttpRepo { get; set; }

    [Inject]
    public HttpInterceptorService Interceptor { get; set; }

    [CascadingParameter]
    public Task<AuthenticationState> AuthState { get; set; }

    [Inject]
    public ToastrService ToastrService { get; set; }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    private bool _isLoading = false;

    private PaginationInfo _paginationInfo = new();
    public int PageNumber { get; set; } = 1;

    private IEnumerable<TyreDto> _tyresDto;


    private TyreFilter _filter = new();


    // RadzenDataGrid<Tyre> grid;

    bool ExportAllPages { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Interceptor.RegisterEvent();
        await Interceptor.RegisterBeforeSendEvent();

        var filter = await LocalStorage.GetItemAsync<TyreFilter>("TyreFilter");
        
        if (filter != null)
        {
            _filter.TyreName = filter.TyreName;
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

        await RefreshTyres();


    }


    private async Task RefreshTyres()
    {
        _isLoading = true;
        // await Task.Delay(1000);

        try
        {

            var vm = await TyreHttpRepo
                .GetFiltered(_filter, PageNumber);

            if (vm != null)
            {
                _tyresDto = vm.TyresDto.Items;



                _paginationInfo = new PaginationInfo
                {
                    PageIndex = vm.TyresDto.PageIndex,
                    TotalCount = vm.TyresDto.TotalCount,
                    TotalPages = vm.TyresDto.TotalPages
                };
            }
        }
        finally
        {
            _isLoading = false;
        }
        StateHasChanged();
    }


    private async void Filter()
    {
        _isLoading = true;
        StateHasChanged();
        await LocalStorage.SetItemAsync("TyreFilter", _filter);
        await RefreshTyres();
        StateHasChanged();

    }


    private async Task OnSelectedPage(int pageNumber)
    {
        PageNumber = pageNumber;
        await RefreshTyres();
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


    private async void DeleteTyre(int id)
    {
        await TyreHttpRepo.Delete(id);
        await RefreshTyres();
        StateHasChanged();
        await ToastrService.ShowInfoMessage($"Opona została usunięta.");
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
