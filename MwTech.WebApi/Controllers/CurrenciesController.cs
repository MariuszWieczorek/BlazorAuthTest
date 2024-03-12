using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MwTech.Application.AccountingPeriods.Commands.AddAccountingPeriod;
using MwTech.Domain.Entities;
using MwTech.Shared.Currencies.Commands.AddCurrency;
using MwTech.Shared.Currencies.Commands.DeleteCurrency;
using MwTech.Shared.Currencies.Commands.EditCurrency;
using MwTech.Shared.Currencies.Dtos;
using MwTech.Shared.Currencies.Dtos.GetEditCurrency;

namespace MwTech.WebApi.Controllers;

[ApiVersion(1)]
[ApiVersion(2)]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class CurrenciesController : BaseApiController
{

    [MapToApiVersion(1)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var currencies = await Mediator
            .Send(new GetCurrenciesQuery
            {
                CurrencyFilter = new CurrencyFilter(),
                PageNumber = 1

            });

        return Ok(currencies);
    }

    [MapToApiVersion(2)]
    [HttpGet]
    public async Task<IActionResult> GetAll2()
    {

        var currencies = new List<Currency> { new Currency { CurrencyCode = "PLN", Name = "Złoty" } };

        return Ok(currencies);
    }

    [MapToApiVersion(1)]
    [HttpPost("page/{pageNumber}")]
    public async Task<IActionResult> GetFiltered([FromBody] CurrencyFilter currencyFilter, int pageNumber)
    {

        if (currencyFilter == null)
        {
            currencyFilter = new CurrencyFilter();
        }

        // return BadRequest();
        // return NotFound();

        var currencies = await Mediator
            .Send(new GetCurrenciesQuery
            {
                CurrencyFilter = currencyFilter,
                PageNumber = pageNumber
            });

        return Ok(currencies);
    }
    
    [MapToApiVersion(1)]
    [HttpPost]
    public async Task<IActionResult> Add(AddCurrencyCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [MapToApiVersion(1)]
    [HttpGet("edit/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var currency = await Mediator.Send(new GetEditCurrencyQuery { Id = id });

        if (currency == null)
        {
            return BadRequest();
        }

        return Ok(currency);
    }

    [MapToApiVersion(1)]
    [HttpPut]
    public async Task<IActionResult> Edit(EditCurrencyCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [MapToApiVersion(1)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCurrencyCommand { Id = id });
        return NoContent();
    }

}
