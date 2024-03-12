using MwTech.Shared.Currencies.Commands.AddCurrency;
using MwTech.Shared.Currencies.Commands.EditCurrency;
using MwTech.Shared.Currencies.Dtos;

namespace MwTech.Blazor.Client.HttpRepository.Interfaces;

public interface ICurrencyHttpRepository
{
    Task Add(AddCurrencyCommand command);
    Task Edit(EditCurrencyCommand command);
    Task Delete(int id);
    Task<CurrenciesViewModel> GetAll();
    Task<CurrenciesViewModel> GetFiltered(CurrencyFilter filter, int pageNumber);
    Task<EditCurrencyCommand> GetEdit(int Id);
}

