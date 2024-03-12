using Microsoft.AspNetCore.Components;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Services;
using MwTech.Shared.Authentication.Commands;

namespace MwTech.Blazor.Client.Pages.Authorization;

public partial class Register
{
    private RegisterUserCommand _command = new RegisterUserCommand();
    private bool _showErrors;
    private IEnumerable<string> _errors;


    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAuthenticationHttpRepository AuthenticationRepo { get; set; }

    [Inject]
    public ToastrService ToastrService { get; set; }

    private async Task Save()
    {
        _showErrors = false;

        await ToastrService.ShowInfoMessage("Save - zostało wywołane");

        
  
        var response = await AuthenticationRepo.RegisterUser(_command);

        if (!response.IsSuccess)
        {
            _errors = new List<string> { response.Errors };
            _showErrors = true;
            return;
        }

        NavigationManager.NavigateTo("/potwierdz-email");

  

    }
}
