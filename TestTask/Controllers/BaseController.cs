using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Controllers;

public class BaseController : ControllerBase
{
    protected readonly ISender _mediator;

    public BaseController(ISender mediator)
    {
        _mediator = mediator;
    }
}
