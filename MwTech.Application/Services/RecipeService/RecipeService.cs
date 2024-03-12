using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Services.RecipeService;

public class RecipeService : IRecipeService
{
    private readonly IApplicationDbContext _context;
    private readonly IProductCostService _productCostService;

    public RecipeService(IApplicationDbContext context, IProductCostService productCostService)
    {
        _context = context;
        _productCostService = productCostService;
    }

    public async Task<IEnumerable<RecipeStage>> GetRecipeVersionStages(int recipeVersionId)
    {
        List<RecipeStage> recipeStages = await GetRecipesStagesWithPositions(recipeVersionId);

        int? prevStageId = null;

        foreach (var recipeStage in recipeStages)

        {
            if (prevStageId != null)
            {
                var prevStage = recipeStages.SingleOrDefault(x => x.Id == prevStageId);
                AddPrevStageSummaryAsNewStagePosition(recipeStage, prevStage);
            }


            await GetCostForStageItems(recipeStage);

            CalculateVolumeForStageItems(recipeStage);

            StageSummariesBeforeChangingVolume(recipeStage);

            CalculateStageItemsForMixerVolume(recipeStage);
            
            StageSummariesAfterChangingVolume(recipeStage);

            CalculateStageItemsPhr(recipeStage);

            CalculateStagePhr(recipeStage);

            CalculateStageDensity(recipeStage);

            CalculateStageRubberContent(recipeStage);



            SortStageItems(recipeStage);

            CalculateRunningQty(recipeStage);

            CalculateLabourMixingCost(recipeStage);

            CalculatePackages(recipeStage);

            CalculateUnitCost(recipeStage);

            prevStageId = recipeStage.Id;
        }

        CalculatePercentFactor(recipeStages);

        return recipeStages;
    }

    private async Task<List<RecipeStage>> GetRecipesStagesWithPositions(int recipeVersionId)
    {
        return await _context.RecipeStages
            .Include(x => x.RecipePositions)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Unit)
            .Include(x => x.RecipePositions)
                .ThenInclude(x => x.RecipePositionPackage)
            .Include(x => x.RecipeVersion)
            .Include(x => x.RecipeVersion.Recipe)
            .Include(x => x.RecipeManuals)
            .Include(x => x.WorkCenter)
            .Include(x => x.LabourClass)
            .Include(x => x.RecipePositionsPackages)
                .ThenInclude(x => x.LabourClass)
             .Include(x => x.RecipePositionsPackages)
                .ThenInclude(x => x.WorkCenter)
            .Where(x => x.RecipeVersionId == recipeVersionId)
            .OrderBy(x => x.StageNo)
            .ToListAsync();
    }

    public async Task<RecipeSummary> GetRecipeSummary(int recipeId)
    {
        RecipeSummary recipeSummary = new RecipeSummary();

        var recipeVersion = await _context.RecipeVersions.SingleOrDefaultAsync(y => y.RecipeId == recipeId && y.DefaultVersion);

        if (recipeVersion != null)
        {
            var stages = await GetRecipeVersionStages(recipeVersion.Id);
            if (stages.Count() != 0)
            {
                var lastStageNo = stages.Max(x => x.StageNo);
                var lastStage = stages.SingleOrDefault(x => x.StageNo == lastStageNo);


                recipeSummary.TotalCost = lastStage.UnitCost.GetValueOrDefault();
                recipeSummary.MaterialCost = lastStage.MaterialUnitCost.GetValueOrDefault();
                recipeSummary.MarkupCost = lastStage.UnitMarkupCost.GetValueOrDefault();
                recipeSummary.LabourCost = lastStage.UnitLabourCost.GetValueOrDefault();
                recipeSummary.TotalQty = lastStage.TotalQty2.GetValueOrDefault();
                recipeSummary.TotalVolume = lastStage.TotalVolume2.GetValueOrDefault();
                recipeSummary.VersionName = lastStage.RecipeVersion.Name;
                recipeSummary.VersionId = lastStage.RecipeVersion.Id;
                recipeSummary.IsAccepted01 = lastStage.RecipeVersion.IsAccepted01;
                recipeSummary.IsAccepted02 = lastStage.RecipeVersion.IsAccepted02;
                recipeSummary.ProductNumber = lastStage.ProductNumber;
                // dodane 2023.11.20
                recipeSummary.RubberContent = lastStage.StagePercentRubberContent.GetValueOrDefault();
                recipeSummary.Density = lastStage.StageDensity.GetValueOrDefault();
                recipeSummary.MaterialUnitCostWithoutProcessReturn = lastStage.MaterialUnitCostWithoutProcessReturn.GetValueOrDefault();
            }
        }

        return recipeSummary;
    }

    public async Task<RecipeSummary> GetRecipeVersionSummary(int recipeId, int recipeVersionId)
    {
        RecipeSummary recipeSummary = new RecipeSummary();

        var recipeVersion = await _context.RecipeVersions.SingleOrDefaultAsync(y => y.RecipeId == recipeId && y.Id == recipeVersionId);
        if (recipeVersion != null)
        {
            var stages = await GetRecipeVersionStages(recipeVersion.Id);
            if (stages.Count() != 0)
            {
                var lastStageNo = stages.Max(x => x.StageNo);
                var lastStage = stages.SingleOrDefault(x => x.StageNo == lastStageNo);

                recipeSummary.TotalCost = lastStage.UnitCost.GetValueOrDefault();
                recipeSummary.MaterialCost = lastStage.MaterialUnitCost.GetValueOrDefault();
                recipeSummary.MarkupCost = lastStage.UnitMarkupCost.GetValueOrDefault();
                recipeSummary.LabourCost = lastStage.UnitLabourCost.GetValueOrDefault();
                recipeSummary.TotalQty = lastStage.TotalQty2.GetValueOrDefault();
                recipeSummary.TotalVolume = lastStage.TotalVolume2.GetValueOrDefault();
                recipeSummary.VersionName = lastStage.RecipeVersion.Name;
                recipeSummary.VersionId = lastStage.RecipeVersion.Id;
                recipeSummary.IsAccepted01 = lastStage.RecipeVersion.IsAccepted01;
                recipeSummary.IsAccepted02 = lastStage.RecipeVersion.IsAccepted02;
                recipeSummary.ProductNumber = lastStage.ProductNumber;
            }
        }

        return recipeSummary;
    }

    private void AddPrevStageSummaryAsNewStagePosition(RecipeStage recipeStage, RecipeStage prevStage)
    {

        decimal? productQty = recipeStage.PrevStageQty ?? prevStage.TotalPhr;

        string productNumber;
        if (prevStage.ProductNumber != null)
            productNumber = prevStage.ProductNumber;
        else
            productNumber = $"{prevStage.RecipeVersion.Recipe.RecipeNumber.Trim()}-{prevStage.StageName}";

        var recipePosition = new RecipePosition
        {
            Id = prevStage.Id,
            RecipeStageId = recipeStage.Id,
            PositionNo = 1,
            Product = new Product
            {
                Density = prevStage.StageDensity.GetValueOrDefault(),
                ContentsOfRubber = prevStage.StagePercentRubberContent.GetValueOrDefault(),
                ScalesId = 1,
                WeightTolerance = 0.2M,
                ProductNumber = productNumber,
                Name = prevStage.ProductName
            },
            ProductQty = productQty.GetValueOrDefault(),
            PrevStageSummary = true,
            MaterialUnitCost = prevStage.MaterialUnitCost.GetValueOrDefault(),
            LabourUnitCost = prevStage.StageUnitLabourCost.GetValueOrDefault() + prevStage.PositionsUnitLabourCost.GetValueOrDefault(),
            MarkupUnitCost = prevStage.StageUnitMarkupCost + prevStage.PositionsUnitMarkupCost.GetValueOrDefault(),
            UnitCost = prevStage.UnitCost.GetValueOrDefault(),

        };

        recipeStage.RecipePositions.Add(recipePosition);
    }

    private async Task GetCostForStageItems(RecipeStage recipeStage)
    {
        foreach (var item in recipeStage.RecipePositions)
        {
            if (!item.PrevStageSummary)
            {

                int? recipeId = await Masterbatch(item.Product);

                if (recipeId != null)
                {
                    var cost = await GetRecipeSummary((int)recipeId);
                    item.MaterialUnitCost = cost.MaterialCost;
                    item.LabourUnitCost = cost.LabourCost;
                    item.MarkupUnitCost = cost.MarkupCost;
                    // dla testu
                    // dodane 2023.11.20
                    item.Product.ContentsOfRubber = cost.RubberContent;
                    item.Product.Density = cost.Density;
                }
                else
                {
                    var partCost = await _productCostService.GetProductPrice(item.ProductId);
                    item.MaterialUnitCost = partCost;
                }
            }

        }
    }

    private async Task<int?> Masterbatch(Product product)
    {
        string recipeNumber = product.ProductNumber.Replace("-1", "");
        // MIE.XB-1 -> MIE.XB
        // MIE.XB-1-TE -> MIE.XB-TE

        //if (productNumber.StartsWith("MIE.X") )

        var category = await _context.ProductCategories
            .SingleOrDefaultAsync(x => x.Id == product.ProductCategoryId);

        if (category?.CategoryNumber == "MIE-0" )
        {
            return _context.Recipes.SingleOrDefault(x => x.RecipeNumber == recipeNumber)?.Id;
        }
        return null;
    }

    private void CalculateVolumeForStageItems(RecipeStage recipeStage)
    {
        foreach (var item in recipeStage.RecipePositions)
        {
            if (item.Product.Density != 0)
                item.PartVolume = Math.Round(item.ProductQty / item.Product.Density, 5);

            item.PartRubberContent = Math.Round(item.ProductQty * item.Product.ContentsOfRubber * 0.01M, 3);

        }
    }

    private void StageSummariesBeforeChangingVolume(RecipeStage recipeStage)
    {
        recipeStage.TotalVolume = recipeStage.RecipePositions.Sum(x => x.PartVolume);;
        recipeStage.TotalRubberContent = recipeStage.RecipePositions.Sum(x => x.PartRubberContent);
        recipeStage.TotalQty = recipeStage.RecipePositions.Sum(x => x.ProductQty);;
        recipeStage.MaterialTotalCost = recipeStage.RecipePositions.Sum(x => x.MaterialTotalCost);
    }

    private void CalculateStageItemsForMixerVolume(RecipeStage recipeStage)
    {
        decimal volumeFactor = 1M;
        decimal qtyFactor = 1M;

        if (recipeStage.TotalVolume != 0)
            volumeFactor = Math.Round(Math.Round(recipeStage.MixerVolume, 2) / Math.Round(recipeStage.TotalVolume.GetValueOrDefault(), 2), 10);

        if (recipeStage.DivideQtyBy != null && recipeStage.MultiplyQtyBy != null)
            if (recipeStage.DivideQtyBy.GetValueOrDefault() != 0 && recipeStage.MultiplyQtyBy.GetValueOrDefault() != 0)
                qtyFactor = recipeStage.MultiplyQtyBy.GetValueOrDefault() / recipeStage.DivideQtyBy.GetValueOrDefault();


        int i = 1;

        foreach (var item in recipeStage.RecipePositions)
        {
            int decimalPlaces = item.Product.DecimalPlaces ?? 2;

            if (item.PrevStageSummary == true)
                decimalPlaces = 1;

            // najpierw wyliczamy nową objętość mnożąc ją przez wyliczone faktory 
            item.PartVolume2 = Math.Round(item.PartVolume.GetValueOrDefault() * volumeFactor * qtyFactor, 5);

            // później na podstawie nowej objętości wyliczamy nową masę mnożąc objętość razy gęstość
            if (item.Product.Density != 0)
                item.PartQty2 = Math.Round(item.PartVolume2.GetValueOrDefault() * item.Product.Density, decimalPlaces);


            item.PartRubberContent2 = Math.Round(item.PartQty2.GetValueOrDefault() * item.Product.ContentsOfRubber * 0.01M, 3);
            item.MaterialTotalCost = Math.Round(item.PartQty2.GetValueOrDefault() * item.MaterialUnitCost.GetValueOrDefault(), 2);
            item.LabourTotalCost = Math.Round(item.PartQty2.GetValueOrDefault() * item.LabourUnitCost.GetValueOrDefault(), 2);
            item.MarkupTotalCost = Math.Round(item.PartQty2.GetValueOrDefault() * item.MarkupUnitCost.GetValueOrDefault(), 2);
        }
    }


    private void StageSummariesAfterChangingVolume(RecipeStage recipeStage)
    {
        recipeStage.TotalVolume2 = recipeStage.RecipePositions.Sum(x => x.PartVolume2);
        recipeStage.TotalQty2 = recipeStage.RecipePositions.Sum(x => x.PartQty2);
        recipeStage.TotalQtyWithoutProcessReturn = recipeStage.RecipePositions.Where(x => x.ReturnFromProcessing == false).Sum(x => x.PartQty2);
        recipeStage.TotalRubberContent2 = Math.Round(recipeStage.RecipePositions.Sum(x => x.PartRubberContent2).GetValueOrDefault(), 3);
        recipeStage.TotalWeightTolerance = Math.Round(recipeStage.RecipePositions.Sum(x => x.Product.WeightTolerance).GetValueOrDefault(), 3);

        decimal unitPerHour = 0M;
        if (recipeStage.StageTimeInSeconds != 0)
        {
            unitPerHour = Math.Round((recipeStage.TotalQty2.GetValueOrDefault() * 3600) / recipeStage.StageTimeInSeconds, 2);
        }
        recipeStage.UnitPerHour = unitPerHour;
    }

    private void CalculateStageItemsPhr(RecipeStage recipeStage)
    {
        foreach (var item in recipeStage.RecipePositions)
        {
            if (recipeStage.TotalRubberContent != 0 && recipeStage.TotalRubberContent != null)
                item.PartPhr = Math.Round((item.ProductQty / recipeStage.TotalRubberContent.GetValueOrDefault()) * 100, 2);
            
            //2023.09.27 - zmiana na prośbę Marcina i Huberta, PHR liczymy z danych wejściowych    
            //item.PartPhr = Math.Round((item.PartQty2.GetValueOrDefault() / recipeStage.TotalRubberContent2.GetValueOrDefault()) * 100, 2);
        }

    }

    private void CalculateStagePhr(RecipeStage recipeStage)
    {
        var totalStagePhr = recipeStage.RecipePositions.Sum(x => x.PartPhr);
        recipeStage.TotalPhr = totalStagePhr;
    }

    private void CalculateStageDensity(RecipeStage recipeStage)
    {
        if (recipeStage.TotalVolume != 0)
            recipeStage.StageDensity = Math.Round(recipeStage.TotalQty.GetValueOrDefault() / recipeStage.TotalVolume.GetValueOrDefault(), 5);
    }

    private void CalculateStageRubberContent(RecipeStage recipeStage)
    {
        if (recipeStage.TotalQty.GetValueOrDefault() != 0)
            recipeStage.StagePercentRubberContent = Math.Round((recipeStage.TotalRubberContent.GetValueOrDefault() / recipeStage.TotalQty.GetValueOrDefault()) * 100, 3);

        /*
        if (recipeStage.TotalQty2.GetValueOrDefault() != 0)
            recipeStage.StagePercentRubberContent = Math.Round((recipeStage.TotalRubberContent2.GetValueOrDefault() / recipeStage.TotalQty2.GetValueOrDefault()) * 100, 2);
        */
    }


    private void SortStageItems(RecipeStage recipeStage)
    {
        foreach (var item in recipeStage.RecipePositions)
        {
            item.PacketNo = item.RecipePositionPackage?.PackageNumber ?? 0;
            // item.PositionNo = 1;
        }

        var rec = recipeStage.RecipePositions = recipeStage.RecipePositions
            .OrderBy(x => x.RecipePositionPackage?.PackageNumber)
            .ThenBy(x => x.PositionNo)
            .ToList();

        recipeStage.RecipePositions = rec;

        recipeStage.RecipeManuals = recipeStage.RecipeManuals
            .OrderBy(x => x.PositionNo)
            .ToList();
    }

    private void CalculateRunningQty(RecipeStage recipeStage)
    {
        decimal runningQty = 0M;
        int scaleId = 0;
        foreach (var item in recipeStage.RecipePositions)
        {
            if (scaleId != item.Product.ScalesId)
            {
                scaleId = item.Product.ScalesId;
                runningQty = item.PartQty2.GetValueOrDefault();
            }
            else
            {
                runningQty += item.PartQty2.GetValueOrDefault();
            }
            item.RunningPartQty2s = runningQty;

        }
    }

    private void CalculateLabourMixingCost(RecipeStage recipeStage)
    {

        decimal labourCostPerHour = recipeStage.LabourClass?.Cost ?? 0;
        decimal labourMarkup = recipeStage.LabourClass?.Markup * 0.01M ?? 0M;
        decimal stageTimeInHour = recipeStage.StageTimeInSeconds / 3600M;
        decimal crewSize = recipeStage.CrewSize.GetValueOrDefault();

        // labourCost = Math.Round(labourCostPerHour * (1 + labourMarkup) * stageTimeInHour * crewSize, 2);
        // rozdzielenie narzutu od robocizny

        decimal labourCost = Math.Round(labourCostPerHour * stageTimeInHour * crewSize, 2);
        decimal markupCost = Math.Round(labourCost * labourMarkup, 2);

        recipeStage.StageLabourMixingCost = labourCost;
        recipeStage.StageMarkupMixingCost = markupCost;
    }

    private void CalculateUnitCost(RecipeStage recipeStage)
    {
        decimal totalQty2 = recipeStage.TotalQty2.GetValueOrDefault();

        // Materials
        decimal materialTotalCost = recipeStage.RecipePositions.Sum(x => x.MaterialTotalCost).GetValueOrDefault();
        recipeStage.MaterialTotalCost = materialTotalCost;
        recipeStage.MaterialTotalCostWithoutProcessReturn = recipeStage.RecipePositions.Where(x => x.ReturnFromProcessing == false).Sum(x => x.MaterialTotalCost).GetValueOrDefault();
        // Labour
        decimal positionsLabourCost = recipeStage.RecipePositions.Sum(x => x.LabourTotalCost).GetValueOrDefault();
        decimal stageLabourMixingCost = recipeStage.StageLabourMixingCost.GetValueOrDefault();
        decimal stageLabourPackingCost = recipeStage.StageLabourPackingCost.GetValueOrDefault();
        decimal stageLabourCost = stageLabourMixingCost + stageLabourPackingCost;
        decimal totalLabourCost = stageLabourMixingCost + stageLabourPackingCost + positionsLabourCost;

        recipeStage.StageLabourCost = stageLabourCost;
        recipeStage.TotalLabourCost = totalLabourCost;
        recipeStage.PositionsLabourCost = positionsLabourCost;



        // Markup 2023.07.24
        decimal positionsMarkupCost = recipeStage.RecipePositions.Sum(x => x.MarkupTotalCost).GetValueOrDefault();
        decimal stageMarkupMixingCost = recipeStage.StageMarkupMixingCost.GetValueOrDefault();
        decimal stageMarkupPackingCost = recipeStage.StageMarkupPackingCost.GetValueOrDefault();
        decimal stageMarkupCost = stageMarkupMixingCost + stageMarkupPackingCost;
        decimal totalMarkupCost = stageMarkupMixingCost + stageMarkupPackingCost + positionsMarkupCost;

        recipeStage.StageMarkupCost = stageMarkupCost;
        recipeStage.TotalMarkupCost = totalMarkupCost;
        recipeStage.PositionsMarkupCost = positionsMarkupCost;
        // Markup 2023.07.24


        //decimal totalCost = materialTotalCost + stageLabourCost + positionsLabourCost;

        decimal totalCost =
              materialTotalCost
            + stageLabourCost + positionsLabourCost
            + stageMarkupCost + positionsMarkupCost;



        recipeStage.TotalCost = totalCost;


        if (totalQty2 != 0)
        {
            recipeStage.StageUnitLabourCost = Math.Round(stageLabourCost / totalQty2, 2);
            recipeStage.PositionsUnitLabourCost = Math.Round(positionsLabourCost / totalQty2, 2);
            recipeStage.UnitLabourCost = Math.Round(totalLabourCost / totalQty2, 2);

            // Markup 2023.07.24
            recipeStage.StageUnitMarkupCost = Math.Round(stageMarkupCost / totalQty2, 2);
            recipeStage.PositionsUnitMarkupCost = Math.Round(positionsMarkupCost / totalQty2, 2);
            recipeStage.UnitMarkupCost = Math.Round(totalMarkupCost / totalQty2, 2);
            // Markup 2023.07.24

            recipeStage.MaterialUnitCost = Math.Round(materialTotalCost / totalQty2, 2);
            recipeStage.UnitCost = Math.Round(totalCost / totalQty2, 2);
            if (recipeStage.TotalQtyWithoutProcessReturn.GetValueOrDefault() != 0)
            {
                recipeStage.MaterialUnitCostWithoutProcessReturn = Math.Round(recipeStage.MaterialTotalCostWithoutProcessReturn.GetValueOrDefault() / recipeStage.TotalQtyWithoutProcessReturn.GetValueOrDefault(), 2);
            }

        }
    }

    private void CalculatePackages(RecipeStage recipeStage)
    {
        recipeStage.StageLabourPackingCost = 0;
        recipeStage.StageMarkupPackingCost = 0;
        foreach (RecipePositionsPackage package in recipeStage.RecipePositionsPackages)
        {
            var totalQty = 0M;


            foreach (var item in recipeStage.RecipePositions)
            {
                if (item.RecipePositionPackageId == package.Id)
                {
                    totalQty = totalQty += item.PartQty2 ?? 0M;
                }
                package.TotalQty = totalQty;
            }


            decimal labourCostPerHour = package.LabourClass?.Cost ?? 0;
            decimal labourMarkup = package.LabourClass?.Markup * 0.01M ?? 0M;
            decimal packingTimeInHour = package.TimeInSeconds / 3600M;
            decimal crewSize = package.CrewSize.GetValueOrDefault();

            decimal labourCost = Math.Round(labourCostPerHour * packingTimeInHour * crewSize, 2);
            decimal markupCost = Math.Round(labourCost * labourMarkup, 2);

            // labourCost = Math.Round(labourCostPerHour * (1 + labourMarkup) * packingTimeInHour * crewSize, 2);
            // rozdzielenie narzutu od robocizny
            package.LabourTotalCost = labourCost;
            package.MarkupTotalCost = markupCost;

            decimal unitPerHour = 0M;
            if (package.TimeInSeconds != 0)
            {
                unitPerHour = Math.Round((package.TotalQty.GetValueOrDefault() * 3600) / package.TimeInSeconds, 2);
            }
            package.UnitPerHour = unitPerHour;

            recipeStage.StageLabourPackingCost += labourCost;
            recipeStage.StageMarkupPackingCost += markupCost;

        }
    }


    private void CalculatePercentFactor(IEnumerable<RecipeStage> recipeStages)
    {
        decimal nextStageFactor = 1m;
        decimal nextStageQty = 0m;
        int count = 1;
        decimal finalQtyWithoutScrap = 0;

        var reverseStages = recipeStages.Reverse();

        foreach (var stage in reverseStages)
        {


            if (count == 1)
                finalQtyWithoutScrap = stage.TotalQtyWithoutProcessReturn.GetValueOrDefault();

            if (count > 1 && stage.TotalQtyWithoutProcessReturn > 0)
                stage.Factor = (nextStageQty * nextStageFactor) / stage.TotalQtyWithoutProcessReturn;
            else
                stage.Factor = 1;


            nextStageFactor = stage.Factor.GetValueOrDefault();
            var x = stage.RecipePositions.Where(x => x.PrevStageSummary).FirstOrDefault();
            if (x != null)
                nextStageQty = x.PartQty2.GetValueOrDefault();
            else
                nextStageQty = 0;


            if (finalQtyWithoutScrap != 0)
            {
                decimal runningPercent = 0M;
                foreach (var item in stage.RecipePositions)
                {
                    if (!item.ReturnFromProcessing)
                    {
                        item.LastStageWeight = item.PartQty2.GetValueOrDefault() * stage.Factor.GetValueOrDefault();
                        item.LastStagePercent = Math.Round((item.LastStageWeight.GetValueOrDefault() / finalQtyWithoutScrap) * 100, 2);
                        runningPercent = runningPercent + item.LastStagePercent.GetValueOrDefault();
                    }

                }
                stage.TotalStagePercent = runningPercent;
            }

            count++;
        }



    }
}
