using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.ProductSettingsVersions.Queries.GetProductsSettingsVersions;
using MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.CopyProductSettingVersionPositions;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetCopyProductSettingVersionPositionsViewModel;

public class GetCopyProductSettingVersionPositionsViewModelQueryHandler : IRequestHandler<GetCopyProductSettingVersionPositionsViewModelQuery, CopyProductSettingVersionPositionsViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetCopyProductSettingVersionPositionsViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CopyProductSettingVersionPositionsViewModel> Handle(GetCopyProductSettingVersionPositionsViewModelQuery request, CancellationToken cancellationToken)
    {

        var product = _context.Products.SingleOrDefault(x => x.Id == request.ProductId);
       
        var copyProductSettingVersionPositionCommand = new CopyProductSettingVersionPositionsCommand
        {
            ProductSettingVersionId = request.ProductSettingVersionId,
            ProductId = request.ProductId,
            ProductNumber = product.ProductNumber,
            ProductName = product.Name,
            SourceProductSettingVersionId = 0
        };


        var productsSettingVersionsViewModel = new ProductsSettingVersionsViewModel
        {
            ProductsSettingsVersions = new List<ProductSettingVersion>(),
            ProductSettingVersionFilter = new ProductSettingVersionFilter(),
            ProductCategories = _context.ProductCategories.OrderBy(x => x.OrdinalNumber).ToList(),
            MachineCategories = _context.MachineCategories.OrderBy(x => x.Name).ToList(),

        };

        var vm = new CopyProductSettingVersionPositionsViewModel
        {
            CopyProductSettingVersionPositionsCommand = copyProductSettingVersionPositionCommand,
            ProductsSettingVersionsViewModel = productsSettingVersionsViewModel
        };

        return vm;

    }
}
