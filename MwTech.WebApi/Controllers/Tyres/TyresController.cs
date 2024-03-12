using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MwTech.WebApi.Controllers;
using MwTech.Shared.Tyres.Tyres.Commands.AddTyre;
using MwTech.Shared.Tyres.Tyres.Models;
using MwTech.Shared.Tyres.Tyres.Queries.GetTyres;
using MwTech.Shared.Tyres.Tyres.Queries.GetEditTyre;
using MwTech.Shared.Tyres.Tyres.Commands.EditTyre;
using MwTech.Shared.Tyres.Tyres.Commands.DeleteTyre;



namespace MwTech.WebApi.Tyres.Controllers;

[ApiVersion(1)]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/tyres")]
[Authorize]
public class TyresController : BaseApiController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tyres = await Mediator
            .Send(new GetTyresQuery
            {
                TyreFilter = new TyreFilter()
            });

        return Ok(tyres);
    }

    [HttpPost("page/{pageNumber}")]
    public async Task<IActionResult> GetFiltered([FromBody] TyreFilter filter, int pageNumber)
    {

        if (filter == null)
        {
            filter = new TyreFilter();
        }

        // return BadRequest();
        // return NotFound();

        var tyresViewModel = await Mediator
            .Send(new GetTyresQuery
            {
                TyreFilter = filter,
                PageNumber = pageNumber
            });

        return Ok(tyresViewModel);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Add(AddTyreCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    
    [HttpGet("edit/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var currency = await Mediator.Send(new GetEditTyreQuery { Id = id });

        if (currency == null)
        {
            return BadRequest();
        }

        return Ok(currency);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(EditTyreCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }




    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTyreCommand { Id = id });
        return NoContent();
    }
}
