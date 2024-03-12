using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Extensions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Tyres;
using MwTech.Shared.Tyres.Tyres.Dtos;
using MwTech.Shared.Tyres.Tyres.Models;
using MwTech.Shared.Tyres.Tyres.Queries.GetTyres;

namespace MwTech.Application.Tyres.Tyres.Queries.GetTyres;

public class GetTyresQueryHandler : IRequestHandler<GetTyresQuery, TyresViewModel>
{
    private readonly ILogger<GetTyresQueryHandler> _logger;
    private readonly IApplicationDbContext _context;

    public GetTyresQueryHandler(ILogger<GetTyresQueryHandler> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<TyresViewModel> Handle(GetTyresQuery request, CancellationToken cancellationToken)
    {
        var Tyres = _context.Tyres
            .AsNoTracking()
            .AsQueryable();

        Tyres = Filter(Tyres, request.TyreFilter);


        var TyresList = Tyres
            .OrderBy(x => x.TyreNumber)
            .Select(x => new TyreDto
            {
                Id = x.Id,
                TyreName = x.Name,
                TyreCode = x.TyreNumber
            });


        int pageNumber = request.PageNumber;
        int pageSize = 100;

        var vm = new TyresViewModel
        {
            TyresDto = await TyresList.PaginatedListAsync(pageNumber, pageSize),
            TyreFilter = request.TyreFilter
        };


        return vm;
    }

    private IQueryable<Tyre> Filter(IQueryable<Tyre> Tyres, TyreFilter filter)
    {
        if (filter != null)
        {
            if (!string.IsNullOrWhiteSpace(filter.TyreName))
                Tyres = Tyres.Where(x => x.Name.Contains(filter.TyreName));

            if (!string.IsNullOrWhiteSpace(filter.TyreNumber))
                Tyres = Tyres.Where(x => x.TyreNumber.ToUpper().Contains(filter.TyreNumber.ToUpper()));

        }

        return Tyres;
    }


}
