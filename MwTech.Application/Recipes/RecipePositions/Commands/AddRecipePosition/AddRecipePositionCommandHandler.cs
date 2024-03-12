using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipePositions.Commands.AddRecipePosition;

public class AddRecipePositionCommandHandler : IRequestHandler<AddRecipePositionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddRecipePositionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddRecipePositionCommand request, CancellationToken cancellationToken)
    {
        var RecipePosition = new RecipePosition
        {
            RecipeStageId = request.RecipeStageId,
            PositionNo = request.PositionNo,
            ProductId = request.ProductId,
            ProductQty = request.ProductQty,
            Description = request.Description,
            PacketNo = request.PacketNo,
            RecipePositionPackageId = request.RecipePositionPackageId,
            ReturnFromProcessing = request.ReturnFromProcessing
        };

        await _context.RecipePositions.AddAsync(RecipePosition);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
