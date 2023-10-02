using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using static Verification.Tracker.Track;

namespace Verification6.WebApi.Filters;

public class ExceptionFilterAttribute : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var AboutContext = context.ActionDescriptor as ControllerActionDescriptor;
        GetInstance().Error($"{AboutContext.ControllerName} | {AboutContext.ActionName} ", context.Exception.Message);
    }
}
