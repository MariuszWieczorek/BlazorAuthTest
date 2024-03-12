using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Ifs.IfsScadaMaterials.Commands.AddReportedMaterialQty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaMaterials.Queries.GetAddReportedMaterialQtyViewModel;

public class GetAddReportedMaterialQtyViewModelQueryHandler : IRequestHandler<GetAddReportedMaterialQtyViewModelQuery, AddReportedMaterialQtyViewModel>
{
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IOracleDbContext _oracleContext;

    public GetAddReportedMaterialQtyViewModelQueryHandler(IScadaIfsDbContext scadaContext, IOracleDbContext oracleContext)
    {
        _scadaContext = scadaContext;
        _oracleContext = oracleContext;
    }
    public async Task<AddReportedMaterialQtyViewModel> Handle(GetAddReportedMaterialQtyViewModelQuery request, CancellationToken cancellationToken)
    {

        var ifsMaterials = _oracleContext.IfsScadaMaterials
                   .FromSqlInterpolated(
                   @$"SELECT *
                   FROM IFSINFO.SCADA_MATERIALS
                   WHERE OP_ID = {request.OP_ID} 
                    AND TRIM(COMPONENT_PART_NO) = TRIM({request.COMPONENT_PART_NO})
                    ")
                 .AsNoTracking()
                 .ToList();

        if (ifsMaterials.Count() != 1)
        {
            throw new Exception($"ilość operacja dla " +
                $"OP_ID = {request.OP_ID} " +
                $"i COMPONENT_PART_NO = {request.COMPONENT_PART_NO} " +
                $"w IFS równa {ifsMaterials.Count()}");
        }

        var ifsMaterial = ifsMaterials
            .Single(x => x.OP_ID == request.OP_ID && x.COMPONENT_PART_NO == request.COMPONENT_PART_NO);

        var addReportedMaterialQtyCommand = new AddReportedMaterialQtyCommand
        {
            OP_ID = request.OP_ID,
            COMPONENT_PART_NO = request.COMPONENT_PART_NO,
            QTY_ISSUED = 0m
        };

        var vm = new AddReportedMaterialQtyViewModel
        {
            AddReportedMaterialQtyCommand = addReportedMaterialQtyCommand,
            IfsScadaMaterial = ifsMaterial
        };

        return vm;
    }
}
