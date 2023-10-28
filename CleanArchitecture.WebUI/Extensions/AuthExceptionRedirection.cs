using CleanArchitecture.WebUI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.WebUI.Extensions
{
    public class AuthExceptionRedirection : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AuthException)
                context.Result = new RedirectToActionResult("Login", "Access", null);
        }
    }
}
