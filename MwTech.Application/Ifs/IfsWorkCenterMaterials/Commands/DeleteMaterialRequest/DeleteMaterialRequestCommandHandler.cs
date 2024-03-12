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

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Commands.DeleteMaterialRequest;

public class DeleteMaterialRequestCommandHandler : IRequestHandler<DeleteMaterialRequestCommand>
{
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public DeleteMaterialRequestCommandHandler(IScadaIfsDbContext scadaContext,
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

    public async Task Handle(DeleteMaterialRequestCommand request, CancellationToken cancellationToken)
    {


        var materialRequest = _context
            .IfsWorkCentersMaterialsRequests
            .SingleOrDefault(x => x.Id == request.ReqId && x.ReqState == 0);



        if (materialRequest != null)
        {
            _context.IfsWorkCentersMaterialsRequests.Remove(materialRequest);
        }


        await _context.SaveChangesAsync();

        return;
    }
}
