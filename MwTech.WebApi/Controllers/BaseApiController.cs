﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MwTech.WebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    protected string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

}
