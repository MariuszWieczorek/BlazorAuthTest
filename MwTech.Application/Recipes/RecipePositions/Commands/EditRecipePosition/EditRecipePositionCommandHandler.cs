using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Queries.GetProducts;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipePositions.Commands.EditRecipePosition;

public class EditRecipePositionCommandHandler : IRequestHandler<EditRecipePositionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditRecipePositionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditRecipePositionCommand request, CancellationToken cancellationToken)
    {
        var recipePosition = await _context.RecipePositions
            .SingleOrDefaultAsync(x => x.Id == request.Id);

        recipePosition.PositionNo = request.PositionNo;
        recipePosition.PacketNo = request.PacketNo;
        recipePosition.ProductId = request.ProductId;
        recipePosition.ProductQty = request.ProductQty;
        recipePosition.Description = request.Description;
        recipePosition.RecipePositionPackageId = request.RecipePositionPackageId;
        recipePosition.ReturnFromProcessing = request.ReturnFromProcessing;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
