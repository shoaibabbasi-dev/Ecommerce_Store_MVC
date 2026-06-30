using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommerceMvcStore.Filters;

/// <summary>
/// Redirects admin users to the product admin dashboard (they must not use customer shop flows).
/// </summary>
public sealed class CustomerOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.IsInRole("Admin"))
        {
            context.Result = new RedirectToActionResult("Index", "Admin", new { area = "" });
        }
    }
}
