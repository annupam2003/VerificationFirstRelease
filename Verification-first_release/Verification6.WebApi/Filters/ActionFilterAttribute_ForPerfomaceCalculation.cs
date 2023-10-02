using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using static Verification.Tracker.Track;

namespace Verification6.WebApi.Filters;

public class ActionFilterAttribute_ForPerfomaceCalculation : ActionFilterAttribute
{
    Stopwatch stopWatch = new Stopwatch();
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        stopWatch.Reset();
        stopWatch.Start();
        base.OnActionExecuting(context);
    }
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
        var AboutContext = context.ActionDescriptor as ControllerActionDescriptor;
        var ActionName = AboutContext.ActionName;
        GetInstance().Trace(stopWatch, $"{AboutContext.ControllerName} | {AboutContext.ActionName} ");
    }
}