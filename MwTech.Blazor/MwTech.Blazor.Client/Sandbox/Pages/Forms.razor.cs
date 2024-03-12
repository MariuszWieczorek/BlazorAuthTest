using Microsoft.AspNetCore.Components;
using MwTech.Blazor.Client.Sandbox.Models;
using MwTech.Blazor.Services;
using Newtonsoft.Json;

namespace MwTech.Blazor.Client.Sandbox.Pages;

public partial class Forms
{
    private bool _isLoading = false;
    private Employee _employee = new Employee
    {
        DateOfEmployment = DateTime.Now
    };

    private IEnumerable<Position> _positions = new List<Position>
    {
        new Position { Id = 1, Name = "IT"},
        new Position { Id = 2, Name = "Magazynier"},
        new Position { Id = 3, Name = "Kierowca"}
    };

    [Inject]
    public ToastrService ToastrService { get; set; }

    private async Task Save()
    {

        int I = 5;
        try
        {
            _isLoading = true;

            await Task.Delay(2000);

            var json = JsonConvert.SerializeObject(_employee);
            await ToastrService.ShowInfoMessage($"Dane zostały zapisane. Użytkownik: {json}.");

            //_employee
            //logika zapisu do bazy danych

            _employee = new Employee
            {
                DateOfEmployment = DateTime.Now
            };
        }
        finally
        {
            _isLoading = false;
        }
    }
}
