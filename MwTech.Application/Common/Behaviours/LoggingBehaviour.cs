using MediatR;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger
        , ICurrentUserService currentUserService
        )
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    // zmiana kolejności parametrów 7.0
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? String.Empty;
        var userName = _currentUserService.UserName ?? String.Empty;

        _logger.LogInformation($"Handling {requestName}");

        _logger.LogInformation("MwTech RequestX: {@Name} {@UserId} {@UserName} {@Request}", requestName, userId, userName, request);



        //        _logger.LogInformation( "B GymManager Request: (1){Name} (2){Request}", requestName, request);
        //       _logger.LogInformation($"C GymManager Request: (1){@requestName} (2){@request}");
        //      _logger.LogInformation($"D GymManager Request: (1){requestName} (2){request}");
        //     _logger.LogInformation( "E GymManager Request: (1){@A} (2){@B}", requestName, request);



        var response = await next();

        _logger.LogInformation($"Handled {typeof(TResponse).Name}");

        return response;
    }

    
}

