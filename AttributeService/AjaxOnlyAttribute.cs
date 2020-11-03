using System;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace AttributeService
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var isAjaxCall = routeContext.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            return isAjaxCall;
        }
    }
}
