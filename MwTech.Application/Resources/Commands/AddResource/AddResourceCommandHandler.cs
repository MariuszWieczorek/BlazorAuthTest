using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Resources.Commands.AddResource;

public class AddResourceCommandHandler : IRequestHandler<AddResourceCommand>
{
    private readonly IApplicationDbContext _context;

    public AddResourceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = new Resource();
        
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
        resource.LaborClassNo = request.LaborClassNo;


        await _context.Resources.AddAsync(resource);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
