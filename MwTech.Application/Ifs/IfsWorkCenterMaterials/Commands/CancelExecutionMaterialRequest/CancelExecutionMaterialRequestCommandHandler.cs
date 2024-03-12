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

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Commands.CancelExecutionMaterialRequest;

public class CancelExecutionMaterialRequestCommandHandler : IRequestHandler<CancelExecutionMaterialRequestCommand>
{
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public CancelExecutionMaterialRequestCommandHandler(IScadaIfsDbContext scadaContext,
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

    public async Task Handle(CancelExecutionMaterialRequestCommand request, CancellationToken cancellationToken)
    {


        var materialRequest = _context
            .IfsWorkCentersMaterialsRequests
            .SingleOrDefault(x => x.Id == request.ReqId && x.ReqState == 1);



        if (materialRequest != null)
        {
            materialRequest.ReqState = 0;
        }


        await _context.SaveChangesAsync();

        return;
    }
}
