using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Resources.Commands.EditResource;

public class EditResourceCommandHandler : IRequestHandler<EditResourceCommand>
{
    private readonly IApplicationDbContext _context;

    public EditResourceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _context.Resources.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        resource.ResourceNumber = request.ResourceNumber;
        resource.Name = request.Name;
        resource.Description = request.Description;
        resource.ProductCategoryId = request.ProductCategoryId;
        resource.UnitId = request.UnitId;
        resource.Cost = request.Cost;
        resource.Markup = request.Markup;
        resource.EstimatedCost = request.EstimatedCost;
        resource.EstimatedMarkup = request.EstimatedMarkup;
        resource.Contract = request.Contract;
        resource.LabourClassId = request.LabourClassId;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
