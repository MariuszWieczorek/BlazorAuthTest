using Microsoft.JSInterop;

namespace MwTech.Blazor.Services;

public class ToastrService
{
    private IJSRuntime _jSRuntime;

    public ToastrService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    public async Task ShowInfoMessage(string message)
    {
        await _jSRuntime.InvokeVoidAsync("toastrFunctions.showToastrInfo", message);
    }
}
