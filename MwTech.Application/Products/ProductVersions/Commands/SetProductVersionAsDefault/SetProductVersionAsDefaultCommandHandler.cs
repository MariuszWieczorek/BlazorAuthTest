using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Commands.SetProductVersionAsDefault;


public class SetProductVersionAsDefaultCommandHandler : IRequestHandler<SetProductVersionAsDefaultCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductVersionAsDefaultCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductVersionAsDefaultCommand request, CancellationToken cancellationToken)
    {
        var productVersions = _context.ProductVersions
            .Where(x => x.ProductId == request.ProductId);
                        
        foreach (var item in productVersions)
        {
            item.DefaultVersion = (item.Id == request.ProductVersionId);
        }

        await _context.SaveChangesAsync();

        return;
    }
}
