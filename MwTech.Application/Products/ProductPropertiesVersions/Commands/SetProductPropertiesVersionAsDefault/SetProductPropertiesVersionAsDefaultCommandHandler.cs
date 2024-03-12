using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.SetProductPropertiesVersionAsDefault;


public class SetProductPropertiesVersionAsDefaultCommandHandler : IRequestHandler<SetProductPropertiesVersionAsDefaultCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductPropertiesVersionAsDefaultCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductPropertiesVersionAsDefaultCommand request, CancellationToken cancellationToken)
    {
        var ProductPropertiesVersions = _context.ProductPropertyVersions
            .Where(x => x.ProductId == request.ProductId);
                        
        foreach (var item in ProductPropertiesVersions)
        {
            item.DefaultVersion = (item.Id == request.ProductPropertiesVersionId);
            item.IsActive = item.DefaultVersion;
        }

        await _context.SaveChangesAsync();

        return;
    }
}
