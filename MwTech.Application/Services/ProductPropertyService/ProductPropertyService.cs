using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Extensions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Enums;
using System.Globalization;
using System.IO;


namespace MwTech.Application.Services.ProductCsvService;

public partial class ProductPropertyService
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ProductPropertyService> _logger;
    //private readonly String _path = @".\ExcelFiles\";
    private readonly string _path = @"\\Kab-svr-fs02\office\Aplikacje\mwbase\pliki_csv\";
    private readonly string _pathOkc = @"maszyna_plaska_csv\";
    private readonly string _pathOdr = @"maszyna_do_drutowki_csv\";
    private readonly string _pathOwu = @"maszyny_do_wulkanizacji_opon_csv_1\";
    private readonly string _pathOrders = @"zlecenia_csv\";


    public ProductPropertyService(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ProductPropertyService> logger
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
    }




    public async Task CalcPrzel(int productId)
    {

        

        var product = _context.Products
            .Include(x => x.ProductCategory)
            .AsNoTracking()
            .SingleOrDefault(x => x.Id == productId);

             


        if (product == null)
        {
            return;
        }

        if (product.ProductCategory.CategoryNumber == "OKC")
        {
            



        }

       

    }





}
