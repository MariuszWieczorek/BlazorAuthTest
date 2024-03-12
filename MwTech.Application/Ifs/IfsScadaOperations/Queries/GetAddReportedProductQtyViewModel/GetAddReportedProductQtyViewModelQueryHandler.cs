using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Ifs.IfsScadaOperations.Commands.AddReportedProductQty;

namespace MwTech.Application.Ifs.IfsScadaOperations.Queries.GetAddReportedProductQtyViewModel;

public class GetAddReportedProductQtyViewModelQueryHandler : IRequestHandler<GetAddReportedProductQtyViewModelQuery, AddReportedProductQtyViewModel>
{
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IOracleDbContext _oracleContext;

    public GetAddReportedProductQtyViewModelQueryHandler(IScadaIfsDbContext scadaContext, IOracleDbContext oracleContext)
    {
        _scadaContext = scadaContext;
        _oracleContext = oracleContext;
    }
    public async Task<AddReportedProductQtyViewModel> Handle(GetAddReportedProductQtyViewModelQuery request, CancellationToken cancellationToken)
    {

        var ifsOperations = _oracleContext.IfsScadaOperations
           .FromSqlInterpolated(
           @$"SELECT *
                   FROM IFSINFO.SCADA_OPERATIONS
                   WHERE OP_ID = {request.OP_ID} 
                    ")
         .AsNoTracking()
         .ToList();

        if (ifsOperations.Count() != 1)
        {
            throw new Exception($"ilość operacja o OP_ID = {request.OP_ID} w IFS równa {ifsOperations.Count()}");
        }

        var ifsOperation = ifsOperations.Single(x => x.OP_ID == request.OP_ID);
        var addReportedProductQtyCommand = new AddReportedProductQtyCommand
        {
            OP_ID = request.OP_ID,
            QTY_REPORTED = 0m,
        };

        var vm = new AddReportedProductQtyViewModel
        {
            AddReportedProductQtyCommand = addReportedProductQtyCommand,
            IfsScadaOperation = ifsOperation
        };

        return vm;
    }
}
