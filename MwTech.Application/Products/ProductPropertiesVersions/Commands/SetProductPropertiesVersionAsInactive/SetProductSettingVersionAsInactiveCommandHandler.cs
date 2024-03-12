using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.SetProductPropertiesVersionAsInactive;


public class SetProductPropertiesVersionAsInactiveCommandHandler : IRequestHandler<SetProductPropertiesVersionAsInactiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductPropertiesVersionAsInactiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductPropertiesVersionAsInactiveCommand request, CancellationToken cancellationToken)
    {
        var propertiesVersion = _context.ProductPropertyVersions
            .SingleOrDefault(x => x.Id == request.ProductPropertiesVersionId && x.ProductId == request.ProductId);
                        
        propertiesVersion.IsActive = false;

        await _context.SaveChangesAsync();

        return;
    }
}
