using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.RoutingTools.Command.ImportRoutingToolsFromIfs;

public class ImportRoutingToolsFromIfsCommandHandler : IRequestHandler<ImportRoutingToolsFromIfsCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IOracleDbContext _oracle;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ImportRoutingToolsFromIfsCommandHandler> _logger;

    public ImportRoutingToolsFromIfsCommandHandler(IApplicationDbContext context,
        IOracleDbContext oracle,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ImportRoutingToolsFromIfsCommandHandler> logger
        )
    {
        _context = context;
        _oracle = oracle;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
    }
    public async Task Handle(ImportRoutingToolsFromIfsCommand request, CancellationToken cancellationToken)
    {

        var ifsRoutingTools = await _oracle.IfsRoutingTools
           .FromSqlRaw($@"SELECT
            TOOL_ID as ToolNumber,
            TOOL_DESCRIPTION as Name
            FROM MANUF_TOOL
            WHERE 1 = 1 
           "
           )
         .AsNoTracking()
         .ToListAsync();



        foreach (var ora in ifsRoutingTools)
        {
            
            

            var mwRoutingTool = await _context.RoutingTools
                .FirstOrDefaultAsync(x => x.ToolNumber == ora.ToolNumber);

            if (mwRoutingTool == null)
            {
                var routingTool = new RoutingTool
                {
                    ToolNumber = ora.ToolNumber,
                    Name = ora.Name
                };

                await _context.RoutingTools.AddAsync(routingTool);
            }

        }

        await _context.SaveChangesAsync();

        return;
    }


}
