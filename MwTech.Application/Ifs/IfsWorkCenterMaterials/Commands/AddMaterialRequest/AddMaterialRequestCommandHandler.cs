using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Commands.AddMaterialRequest;

public class AddMaterialRequestCommandHandler : IRequestHandler<AddMaterialRequestCommand>
{
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public AddMaterialRequestCommandHandler(IScadaIfsDbContext scadaContext,
        IOracleDbContext oracleContext,
        IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _scadaContext = scadaContext;
        _oracleContext = oracleContext;
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(AddMaterialRequestCommand request, CancellationToken cancellationToken)
    {


        var materialRequest = _context
            .IfsWorkCentersMaterialsRequests
            .SingleOrDefault(x => x.OrderNo == request.OrderNo && x.WorkCenterNo == request.WorkCenterNo && x.PartNo == request.PartNo && x.ReqState == 0);

        var qtyRequired = request.QtyRequired == 0 ? 1 : request.QtyRequired;

        if (materialRequest == null)
        {
            materialRequest = new IfsWorkCenterMaterialRequest
            {
                OrderNo = request.OrderNo,
                WorkCenterNo = request.WorkCenterNo,
                PartNo = request.PartNo,
                QtyRequired = qtyRequired,
                ReqDate = _dateTimeService.Now,
                SourceLocation = request.SourceLocation
            };

            await _context.IfsWorkCentersMaterialsRequests.AddAsync(materialRequest);
        }
        else
        {
            materialRequest.QtyRequired = qtyRequired;
            materialRequest.ReqDate = _dateTimeService.Now;
        }


        await _context.SaveChangesAsync();

        return;
    }
}
