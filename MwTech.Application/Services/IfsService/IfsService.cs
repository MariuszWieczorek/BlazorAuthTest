using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Services.IfsService;

public class IfsService : IIfsService
{
    private readonly IApplicationDbContext _context;
    private readonly IOracleDbContext _oracle;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<IfsService> _logger;

    public IfsService(IApplicationDbContext context,
        IOracleDbContext oracle,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<IfsService> logger
        )
    {
        _context = context;
        _oracle = oracle;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<IEnumerable<IfsInventoryPartInStock>>  GetIfsInventoryPartInStock(string partNo)
    {
        var ifsInventoryPartsInStock = _oracle.IfsInventoryPartsInStock
                  .FromSqlRaw(
                  @$"
                    SELECT x.""PartNo"", x.""LocationNo"", x.""LocationName"",
                           x.""QtyOnHand"", x.""QtyReserved"", x.""QtyInTransit"", x.""QtyAvailable"", 
                           x.""ReceiptDate"", x.""HandlingUnitId""
                    FROM (  
                    SELECT PART_NO as ""PartNo"",
                    LOCATION_NO as ""LocationNo"",
                    Inventory_Location_API.Get_Location_Name(CONTRACT, LOCATION_NO) as ""LocationName"",
                    SUM(QTY_ONHAND) as ""QtyOnHand"", 
                    SUM(QTY_RESERVED) as ""QtyReserved"",
                    SUM(QTY_IN_TRANSIT) as ""QtyInTransit"", 
                    SUM(QTY_ONHAND) - QTY_RESERVED as ""QtyAvailable"",
                    MAX(RECEIPT_DATE) as ""ReceiptDate"",
                    HANDLING_UNIT_ID as ""HandlingUnitId""
                    from ifsapp.INVENTORY_PART_IN_STOCK_UIV st
                    WHERE QTY_ONHAND - QTY_RESERVED > 0
                    GROUP BY PART_NO,LOCATION_NO,QTY_ONHAND,QTY_RESERVED,QTY_IN_TRANSIT,CONTRACT, HANDLING_UNIT_ID
                    ) x
                   ")
                  .AsNoTracking()
                  .AsQueryable();


        var filter = new IfsInventoryPartInStockFilter()
        {
            PartNo = partNo
        };


        ifsInventoryPartsInStock = Filter(ifsInventoryPartsInStock, filter);

        var inventoryPartsInStock = await ifsInventoryPartsInStock
                .OrderBy(x => x.LocationNo)
                .Where(x=>!x.LocationNo.Contains(".BR"))
                .ToListAsync();


        return inventoryPartsInStock;

    }


    public async Task<IEnumerable<IfsInventoryPartInStock>> GetIfsInventoryPartInStock2(string partNo)
    {
        var ifsInventoryPartsInStock = _oracle.IfsInventoryPartsInStock
                  .FromSqlRaw(
                  @$"
                    SELECT x.""PartNo"", x.""LocationNo"", x.""LocationName"",
                           x.""QtyOnHand"", x.""QtyReserved"", x.""QtyInTransit"", x.""QtyAvailable"", 
                           x.""ReceiptDate"", x.""HandlingUnitId""
                    FROM (  
                    SELECT PART_NO as ""PartNo"",
                    LOCATION_NO as ""LocationNo"",
                    Inventory_Location_API.Get_Location_Name(CONTRACT, LOCATION_NO) as ""LocationName"",
                    SUM(QTY_ONHAND) as ""QtyOnHand"", 
                    SUM(QTY_RESERVED) as ""QtyReserved"",
                    SUM(QTY_IN_TRANSIT) as ""QtyInTransit"", 
                    SUM(QTY_ONHAND) - QTY_RESERVED as ""QtyAvailable"",
                    MAX(RECEIPT_DATE) as ""ReceiptDate"",
                    HANDLING_UNIT_ID as ""HandlingUnitId""
                    from ifsapp.INVENTORY_PART_IN_STOCK_UIV st
                    WHERE QTY_ONHAND - QTY_RESERVED > 0
                    GROUP BY PART_NO,LOCATION_NO,QTY_ONHAND,QTY_RESERVED,QTY_IN_TRANSIT,CONTRACT, HANDLING_UNIT_ID
                    ) x
                   ")
                  .AsNoTracking()
                  .AsQueryable();


        var filter = new IfsInventoryPartInStockFilter()
        {
            PartNo = partNo
        };


        ifsInventoryPartsInStock = Filter(ifsInventoryPartsInStock, filter);

        var inventoryPartsInStock = await ifsInventoryPartsInStock
                .OrderBy(x => x.LocationNo)
                .Where(x => !x.LocationNo.Contains(".BR"))
                .ToListAsync();


        return inventoryPartsInStock;

    }
    private IQueryable<IfsInventoryPartInStock> Filter(IQueryable<IfsInventoryPartInStock> inventoryPartsInStock, IfsInventoryPartInStockFilter filter)
    {
        if (filter != null)
        {
            if (!string.IsNullOrWhiteSpace(filter.PartNo))
                inventoryPartsInStock = inventoryPartsInStock.Where(x => x.PartNo.ToUpper().Contains(filter.PartNo.ToUpper()));
        }

        return inventoryPartsInStock;
    }
}
