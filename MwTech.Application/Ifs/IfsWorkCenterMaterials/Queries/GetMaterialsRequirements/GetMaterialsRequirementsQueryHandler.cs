using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Ifs;
using System.Linq.Dynamic.Core;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Queries.GetMaterialsRequirements;

public class GetMaterialsRequirementsQueryHandler : IRequestHandler<GetMaterialsRequirementsQuery, MaterialsRequirementsViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IProductService _productWeightService;
    private readonly IIfsService _ifsService;

    public GetMaterialsRequirementsQueryHandler(IOracleDbContext oracleContext,
        IApplicationDbContext context,
        IScadaIfsDbContext scadaContext,
        IProductService productWeightService,
        IIfsService ifsService
        )
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
        _productWeightService = productWeightService;
        _ifsService = ifsService;
    }
    public async Task<MaterialsRequirementsViewModel> Handle(GetMaterialsRequirementsQuery request, CancellationToken cancellationToken)
    {


        var materialTransportRequests = _context.IfsWorkCentersMaterialsRequests
                   .Where(x => x.ReqState < 2)
                   .AsNoTracking()
                   .AsQueryable();


        //SHOP_ORDER_OPERATION_API.GET_REMAINING_QTY(ORDER_NO, RELEASE_NO, SEQUENCE_NO, OPERATION_NO) AS REMAINING_QTY

        materialTransportRequests = Filter(materialTransportRequests, request.MaterialRequirementFilter);

        var ifsMaterialsList = await materialTransportRequests
            .OrderBy(x => x.OrderNo)
            .ToListAsync();



        foreach (var item in ifsMaterialsList)
        {
            var lokalizations = await _ifsService.GetIfsInventoryPartInStock(item.PartNo);

            item.IfsInventoryPartsInStock.AddRange(lokalizations);

        }





        var vm = new MaterialsRequirementsViewModel
        {
            MaterialRequirementFilter = request.MaterialRequirementFilter,
            IfsWorkCenterMaterialRequests = ifsMaterialsList,

        };

        return vm;

    }

    public IQueryable<IfsWorkCenterMaterialRequest> Filter(IQueryable<IfsWorkCenterMaterialRequest> materials, MaterialRequirementFilter filter)
    {
        if (filter != null)
        {
            if (!string.IsNullOrWhiteSpace(filter.PartNo))
                materials = materials.Where(x => x.PartNo.ToUpper().Contains(filter.PartNo.ToUpper()));

            if (!string.IsNullOrWhiteSpace(filter.OrderNo))
                materials = materials.Where(x => x.OrderNo.ToUpper().Contains(filter.OrderNo.ToUpper()));

            /*
            if (!string.IsNullOrWhiteSpace(filter.WorkCenterNo))
                materials = materials.Where(x => x.WorkCenterNo.ToUpper().Contains(filter.WorkCenterNo.ToUpper()));
            */

            if (!string.IsNullOrWhiteSpace(filter.WorkCenterNo))
                if (filter.WorkCenterNo.Contains("%") || filter.WorkCenterNo.Contains("_"))
                {
                    materials = materials.Where(x => EF.Functions.Like(x.WorkCenterNo, filter.WorkCenterNo));
                }
                else
                {
                    materials = materials.Where(x => x.WorkCenterNo.ToUpper().Contains(filter.WorkCenterNo.ToUpper()));
                }


        }

        return materials;
    }
}
