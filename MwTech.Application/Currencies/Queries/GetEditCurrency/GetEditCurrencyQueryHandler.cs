using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Currencies.Commands.EditCurrency;
using MwTech.Domain.Entities;
using MwTech.Shared.Currencies.Commands.EditCurrency;
using MwTech.Shared.Currencies.Dtos.GetEditCurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Currencies.Queries.GetEditCurrency;

public class GetEditCurrencyQueryHandler : IRequestHandler<GetEditCurrencyQuery, EditCurrencyCommand>
{
    private readonly IApplicationDbContext _context;

    public GetEditCurrencyQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditCurrencyCommand> Handle(GetEditCurrencyQuery request, CancellationToken cancellationToken)
    {

        var currencyToEdit =  _context.Currencies.SingleOrDefault(x => x.Id == request.Id);
        
        if (currencyToEdit == null)
        {
            return null; 
        }

        return new EditCurrencyCommand 
        {
            Id = currencyToEdit.Id,
            CurrencyCode = currencyToEdit.CurrencyCode,
            Name = currencyToEdit.Name
        };
    }
    
}
