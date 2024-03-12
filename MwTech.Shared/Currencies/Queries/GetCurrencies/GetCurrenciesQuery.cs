using MediatR;

namespace MwTech.Shared.Currencies.Dtos;

public class GetCurrenciesQuery : IRequest<CurrenciesViewModel> 
{
    public CurrencyFilter CurrencyFilter { get; set; }
    public string SearchValue { get; set; }
    public string OrderInfo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
