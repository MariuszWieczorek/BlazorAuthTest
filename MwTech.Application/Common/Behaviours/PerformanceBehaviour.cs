using MwTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MwTech.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
{
    private readonly ILogger _logger;
    // private readonly ICurrentUserService _currentUserService;
    private readonly Stopwatch _timer;

    public PerformanceBehaviour(ILogger<TRequest> logger
        //, ICurrentUserService  currentUserService
        )
    {
        _timer = new Stopwatch();
        _logger = logger;
       // _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        int maxMilliseconds = 500;
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var userId = "TestUserId"; //_currentUserService.UserId ?? String.Empty;
        var userName = "TestUserName"; //_currentUserService.UserName ?? String.Empty;

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds < maxMilliseconds)
        {
            _logger.LogInformation("MwTech Long Running Request : {@Name} {@UserId} {@UserName} ({@ElapsedMilliseconds} milliseconds) {@Request}",
                 typeof(TRequest).Name, userId, userName, elapsedMilliseconds, request);
        }

        return response;
    }
}

