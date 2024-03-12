using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MwTech.Shared.Authentication.Commands;
using MwTech.Shared.Authentication.Dtos;

namespace MwTech.WebApi.Controllers;

[ApiVersion("1")]
[ApiExplorerSettings(GroupName = "v1")]
public class TokenController : BaseApiController
{
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshTokenCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(new LoginUserDto
            {
                ErrorMessage = "Nieprawidłowe dane."
            });

        var result = await Mediator.Send(command);

        return Ok(result);
    }
}
