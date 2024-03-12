using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Shared.Tyres.Tyres.Commands.AddTyre;
using MwTech.Shared.Tyres.Tyres.Commands.EditTyre;
using MwTech.Shared.Tyres.Tyres.Models;
using MwTech.Shared.Tyres.Tyres.Queries.GetTyres;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MwTech.Blazor.Client.HttpRepository;

public class TyreHttpRepository : ITyreHttpRepository
{
    private readonly HttpClient _client;

    public TyreHttpRepository(HttpClient client)
    {
        _client = client;
    }
    
    // Pobieranie walut nietypowo przez HttpPost
    // w body przekazuję filter
    public async Task<TyresViewModel> GetFiltered(TyreFilter filter, int pageNumber = 1)
    {
        if (filter == null)
            filter = new TyreFilter();

        await Console.Out.WriteLineAsync("Opony endpoint - przed");
        var response = await _client.PostAsJsonAsync($"v1/tyres/page/{pageNumber}",filter);
        await Console.Out.WriteLineAsync("Opony endpoint - po");
        var responseBody =  await response.Content.ReadAsStringAsync();
        var vm = JsonConvert.DeserializeObject<TyresViewModel>(responseBody);

        return vm;
    }


    public async Task<TyresViewModel> GetAll()
    {
        var vm = await _client.GetFromJsonAsync<TyresViewModel>("v1/tyres");
        return vm;
    }
    
    public async Task Add(AddTyreCommand command)
        => await _client.PostAsJsonAsync("v1/tyres", command);

    
    public async Task<EditTyreCommand> GetEdit(int Id)
        => await _client.GetFromJsonAsync<EditTyreCommand>($"v1/tyres/edit/{Id}");


    public async Task Edit(EditTyreCommand command)
        => await _client.PutAsJsonAsync("v1/tyres", command);


    public async Task Delete(int id)
        => await _client.DeleteAsync($"v1/tyres/{id}");

}

