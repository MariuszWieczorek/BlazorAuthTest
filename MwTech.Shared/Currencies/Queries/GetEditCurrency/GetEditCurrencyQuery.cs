using MediatR;
using MwTech.Shared.Currencies.Commands.EditCurrency;
using MwTech.Domain.Entities;

namespace MwTech.Shared.Currencies.Dtos.GetEditCurrency;

public class GetEditCurrencyQuery : IRequest<EditCurrencyCommand> 
{
    public int Id { get; set; }
}
