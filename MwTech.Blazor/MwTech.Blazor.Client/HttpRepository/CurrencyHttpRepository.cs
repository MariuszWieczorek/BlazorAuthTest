using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Shared.Currencies.Commands.AddCurrency;
using MwTech.Shared.Currencies.Commands.EditCurrency;
using MwTech.Shared.Currencies.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MwTech.Blazor.Client.HttpRepository;

public class CurrencyHttpRepository : ICurrencyHttpRepository
{
    private readonly HttpClient _client;

    public CurrencyHttpRepository(HttpClient client)
    {
        _client = client;
    }
    
    // Pobieranie walut nietypowo przez HttpPost
    // w body przekazuję filter
    public async Task<CurrenciesViewModel> GetFiltered(CurrencyFilter filter, int pageNumber = 1)
    {
        if (filter == null)
            filter = new CurrencyFilter();

        var response = await _client.PostAsJsonAsync($"v1/currencies/page/{pageNumber}",filter);
        var responseBody =  await response.Content.ReadAsStringAsync();
        var vm = JsonConvert.DeserializeObject<CurrenciesViewModel>(responseBody);

        return vm;
    }


    public async Task<CurrenciesViewModel> GetAll()
    {
        var vm = await _client.GetFromJsonAsync<CurrenciesViewModel>("v1/currencies");
        return vm;
    }

    public async Task Add(AddCurrencyCommand command)
        => await _client.PostAsJsonAsync("v1/currencies", command);


    public async Task<EditCurrencyCommand> GetEdit(int Id)
        => await _client.GetFromJsonAsync<EditCurrencyCommand>($"v1/currencies/edit/{Id}");

    public async Task Edit(EditCurrencyCommand command)
        => await _client.PutAsJsonAsync("v1/currencies", command);

    public async Task Delete(int id)
        => await _client.DeleteAsync($"v1/currencies/{id}");
}



// return await _client.GetFromJsonAsync<CurrenciesViewModel>($"products?pageNumber={pageNumber}&orderInfo={orderInfo}&searchValue={searchValue}");

/*
var serialFilter = JsonConvert.SerializeObject(filter);
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("v1/currencies"),
    Content = new StringContent(serialFilter, Encoding.UTF8, MediaTypeNames.Application.Json),
};
var response = await _client.SendAsync(request);
var responseBody = await response.Content.ReadAsStringAsync();
var ret2 = JsonConvert.DeserializeObject<CurrenciesViewModel>(responseBody);
*/