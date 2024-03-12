using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using System.Linq.Dynamic.Core;
using System.Data.Common;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Queries.GetIfsWorkCenterMaterials;

public class GetIfsWorkCenterMaterialsQueryHandler : IRequestHandler<GetIfsWorkCenterMaterialsQuery, IfsWorkCenterMaterialsViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IProductService _productWeightService;

    public GetIfsWorkCenterMaterialsQueryHandler(IOracleDbContext oracleContext,
        IApplicationDbContext context,
        IScadaIfsDbContext scadaContext,
        IProductService productWeightService
        )
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
        _productWeightService = productWeightService;
    }
    public async Task<IfsWorkCenterMaterialsViewModel> Handle(GetIfsWorkCenterMaterialsQuery request, CancellationToken cancellationToken)
    {



        var ifsMaterials = _oracleContext.IfsWorkCenterMaterials
                   .FromSqlRaw(
                   @$"
                   SELECT x.""OrderNo"", x.""SetNo"", x.""WorkCenterNo"", x.""OperationDescription"", x.""RevisedQtyDue"",
                          x.""QtyComplete"", x.""OpStartDate"", x.""OpFinishDate"", x.""PartNo"", 
                          x.""QtyRequired"", x.""QtyAvailable"", x.""QtyOnInboundLocations"", x.""PrintUnit"", x.""SourceLocation"", x.""ProposedLocation"",
                          x.""OperationState"", x.""MaterialState"", x.""OperStatusCode"", x.""Shift""  
                   FROM (  
                   select 
                   o.ORDER_NO as ""OrderNo"",
                   o.PART_NO as ""SetNo"",
                   o.WORK_CENTER_NO as ""WorkCenterNo"",
                   o.OPERATION_DESCRIPTION as ""OperationDescription"",
                   o.REVISED_QTY_DUE as ""RevisedQtyDue"",
                   o.QTY_COMPLETE as ""QtyComplete"",
                   o.OP_START_DATE as ""OpStartDate"",
                   o.OP_FINISH_DATE as ""OpFinishDate"",
                   m.part_no as ""PartNo"",
                   m.qty_required as ""QtyRequired"",
                   Shop_Material_Alloc_Util_API.Get_Qty_Avail_To_Issue(m.ORDER_NO, m.RELEASE_NO, m.SEQUENCE_NO, m.LINE_ITEM_NO) as ""QtyAvailable"",
                   Shop_Material_Alloc_Util_API.Get_Qty_Avail_To_Issue(m.ORDER_NO, m.RELEASE_NO, m.SEQUENCE_NO, m.LINE_ITEM_NO, 'TRUE') as ""QtyOnInboundLocations"",
                   m.PRINT_UNIT as ""PrintUnit"",
                   m.CF$_C_SOURCE_LOKC as ""SourceLocation"",
                   m.CF$_C_PROPOSED_LOCATION as ""ProposedLocation"",
                   o.ROWSTATE as ""OperationState"", 
                   o.OPER_STATUS_CODE as ""OperStatusCode"",
                   m.OBJSTATE as ""MaterialState"",
                   o.CF$_SHIFT as ""Shift""
                   from ifsapp.SO_OPER_DISPATCH_LIST_cfv o
                   inner join SHOP_MATERIAL_ALLOC_cfv  m
                   on o.Order_no = m.order_no
                   where 1 = 1 
                   and o.ROWSTATE in ('Released','Started','Planned')
                   and o.OPER_STATUS_CODE not in ('Closed')
                   and (
                   to_char(o.OP_START_DATE,'MM.DD.YYYY') = to_char(SYSDATE,'MM.DD.YYYY')
                   OR 
                   to_char(o.OP_START_DATE,'MM.DD.YYYY') = to_char(SYSDATE - 1,'MM.DD.YYYY')
                   ) 
                   and m.OBJSTATE in ('Released','Started','Planned','Issued')
                   -- and o.work_center_no = 'KOMPL'
                    ) x
                   ")
                   .AsNoTracking()
                   .AsQueryable();

        //SHOP_ORDER_OPERATION_API.GET_REMAINING_QTY(ORDER_NO, RELEASE_NO, SEQUENCE_NO, OPERATION_NO) AS REMAINING_QTY

        ifsMaterials = Filter(ifsMaterials, request.IfsWorkCenterMaterialFilter);

        var ifsMaterialsList = await ifsMaterials
            .OrderBy(x => x.OrderNo)
            .ToListAsync();



        foreach (var item in ifsMaterialsList)
        {
            var qtyReported = _context.IfsWorkCentersMaterialsRequests
                   .FirstOrDefault(x => x.OrderNo == item.OrderNo && x.WorkCenterNo == item.WorkCenterNo && x.PartNo == item.PartNo && x.ReqState < 2);

            if (qtyReported != null)
            {
                item.ReportedQty = qtyReported.QtyRequired;
                item.ReqId = qtyReported.Id;
                item.ReqState = qtyReported.ReqState;
            }

        }


        if (request.Supplier)
        {
            ifsMaterialsList = ifsMaterialsList
                .Where(x => x.ReportedQty > 0).ToList();
        }


        var vm = new IfsWorkCenterMaterialsViewModel
        {
            IfsWorkCenterMaterialFilter = request.IfsWorkCenterMaterialFilter,
            IfsWorkCenterMaterials = ifsMaterialsList,

        };

        return vm;

    }

    public IQueryable<IfsWorkCenterMaterial> Filter(IQueryable<IfsWorkCenterMaterial> materials, IfsWorkCenterMaterialFilter filter)
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

            if (!string.IsNullOrWhiteSpace(filter.Shift))
                materials = materials.Where(x => x.Shift.ToUpper().Contains(filter.Shift.ToUpper()));


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
