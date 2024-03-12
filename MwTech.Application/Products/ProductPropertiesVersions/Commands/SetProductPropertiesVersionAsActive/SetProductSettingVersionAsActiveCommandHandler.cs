using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.SetProductPropertiesVersionAsActive;


public class SetProductPropertiesVersionAsActiveCommandHandler : IRequestHandler<SetProductPropertiesVersionAsActiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductPropertiesVersionAsActiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductPropertiesVersionAsActiveCommand request, CancellationToken cancellationToken)
    {
        var propertiesVersion = _context.ProductPropertyVersions
            .SingleOrDefault(x => x.Id == request.ProductPropertiesVersionId && x.ProductId == request.ProductId);
                        
        propertiesVersion.IsActive = true;

        await _context.SaveChangesAsync();

        return;
    }
}
