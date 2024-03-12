using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;
using System.IO.Packaging;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.CopyRecipeVersion;

public class CopyRecipeVersionCommandHandler : IRequestHandler<CopyRecipeVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;

    public CopyRecipeVersionCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUser)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
    }

    public async Task Handle(CopyRecipeVersionCommand request, CancellationToken cancellationToken)
    {


        var Recipe = await _context.Recipes
            .SingleAsync(x => x.Id == request.RecipeId);



        await copyRecipeVersion(request);


        await _context.SaveChangesAsync();

        return;

    }

    private async Task copyRecipeVersion(CopyRecipeVersionCommand request)
    {
        var RecipeVersion = await _context.RecipeVersions
                    .SingleOrDefaultAsync(x => x.RecipeId == request.RecipeId && x.Id == request.RecipeVersionId);




        var newRecipeVersion = new RecipeVersion
        {
            RecipeId = request.RecipeId,
            VersionNumber = RecipeVersion.VersionNumber,
            AlternativeNo = RecipeVersion.AlternativeNo,
            Name = RecipeVersion.Name,
            Description = RecipeVersion.Description,
            DefaultVersion = false,

            IsAccepted01 = false,
            Accepted01ByUserId = null,
            Accepted01Date = null,

            IsAccepted02 = false,
            Accepted02ByUser = null,
            Accepted02Date = null,

            CreatedByUserId = _currentUser.UserId,
            CreatedDate = _dateTimeService.Now,

            ModifiedByUser = null,
            ModifiedDate = null,

            RecipeQty = RecipeVersion.RecipeQty,

            IsActive = true
        };


        _context.RecipeVersions.Add(newRecipeVersion);

        await copyRecipeStages(request, newRecipeVersion);
        // await copyRecipeManuals(request, newRecipeVersion);

    }





    private async Task copyRecipeStages(CopyRecipeVersionCommand request, RecipeVersion newRecipeVersion)
    {


        var recipeStagesToCopy = await _context.RecipeStages
                    .Include(x => x.RecipeVersion)
                    .Include(x => x.RecipePositions)
                    .ThenInclude(x => x.RecipePositionPackage)
                    .Include(x => x.RecipePositionsPackages)
                    .Include(x => x.RecipeManuals)
                    .Where(x => x.RecipeVersion.RecipeId == request.RecipeId && x.RecipeVersionId == request.RecipeVersionId)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var recipeStage in recipeStagesToCopy)
        {

            var newStage = new RecipeStage
            {
                StageNo = recipeStage.StageNo,
                StageName = recipeStage.StageName,
                ProductNumber = recipeStage.ProductNumber,
                ProductName = recipeStage.ProductName,
                RecipeVersion = newRecipeVersion,
                MixerVolume = recipeStage.MixerVolume,
                Factor = recipeStage.Factor,
                PrevStageQty = recipeStage.PrevStageQty,
                WorkCenterId = recipeStage.WorkCenterId,
                LabourClassId = recipeStage.LabourClassId,
                CrewSize = recipeStage.CrewSize,
                LabourRunFactor = recipeStage.LabourRunFactor,
                StageTimeInSeconds = recipeStage.StageTimeInSeconds,
                MultiplyQtyBy = recipeStage.MultiplyQtyBy,
                DivideQtyBy = recipeStage.DivideQtyBy
            };

            _context.RecipeStages.Add(newStage);



            var listOfPackages = CopyRecipePositionsPackages(recipeStage, newStage);
            CopyRecipePositions(recipeStage, newStage, listOfPackages);
            CopyRecipeManuals(recipeStage, newStage);

        }

        await _context.SaveChangesAsync();
    }

    private void CopyRecipePositions(RecipeStage recipeStage, RecipeStage newStage, List<RecipePositionsPackage> listOfPackages)
    {
        foreach (var item in recipeStage.RecipePositions)
        {


            var newRecipePosition = new RecipePosition
            {
                RecipeStage = newStage,
                PositionNo = item.PositionNo,
                ProductId = item.ProductId,
                ProductQty = item.ProductQty,

            };

            if (item.RecipePositionPackage != null)
            {
                var number = item.RecipePositionPackage.PackageNumber;
                var package = listOfPackages.FirstOrDefault(x => x.RecipeStage == newStage && x.PackageNumber == number);
                newRecipePosition.RecipePositionPackage = package;

            }

            _context.RecipePositions.Add(newRecipePosition);
        }
    }


    private List<RecipePositionsPackage> CopyRecipePositionsPackages(RecipeStage recipeStage, RecipeStage newStage)
    {
        var listOfNewPackages = new List<RecipePositionsPackage>();

        foreach (var item in recipeStage.RecipePositionsPackages)
        {
            var newRecipePositionsPackage = new RecipePositionsPackage
            {
                RecipeStage = newStage,
                PackageNumber = item.PackageNumber,
                ProductNumber = item.ProductNumber,
                ProductName = item.ProductName,
                Description = item.Description,
                WorkCenterId = item.WorkCenterId,
                LabourClassId = item.LabourClassId,
                CrewSize = item.CrewSize,
                TimeInSeconds = item.TimeInSeconds,
            };

            _context.RecipePositionsPackages.Add(newRecipePositionsPackage);
            listOfNewPackages.Add(newRecipePositionsPackage);
        }
        return listOfNewPackages;
    }


    private void CopyRecipeManuals(RecipeStage recipeStage, RecipeStage newStage)
    {
        foreach (var recipeManual in recipeStage.RecipeManuals)
        {

            var newRecipeManual = new RecipeManual
            {
                RecipeStage = newStage,
                RecipeVersion = newStage.RecipeVersion,
                PositionNo = recipeManual.PositionNo,
                TextValue = recipeManual.TextValue,
                Description = recipeManual.Description,
                Duration = recipeManual.Duration
            };

            _context.RecipeManuals.Add(newRecipeManual);
        }
    }


}
