using LinkServer.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LinkServer.Filters
{
    public class AuthenticateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the session contains a valid "username"
            var username = context.HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(username))
            {
                context.Result = new UnauthorizedObjectResult("User is not authenticated.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing after the action executes
            // Must be here to use IActionFilter
        }
    }
}