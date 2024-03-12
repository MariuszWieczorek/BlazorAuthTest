using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Commands.SetProductVersionAsInactive;


public class SetProductVersionAsInactiveCommandHandler : IRequestHandler<SetProductVersionAsInactiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductVersionAsInactiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductVersionAsInactiveCommand request, CancellationToken cancellationToken)
    {
        var routeVersion = _context.ProductVersions
            .SingleOrDefault(x => x.Id == request.ProductVersionId && x.ProductId == request.ProductId);
                        
        routeVersion.IsActive = false;

        await _context.SaveChangesAsync();

        return;
    }
}
