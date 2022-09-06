using ApplicationServices.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodAppServer.Extensions;

public class ApiValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.SelectMany(x => x.Value!.Errors)
                .Select(x => x.ErrorMessage);

            context.Result = new BadRequestObjectResult(new ApiErrorResult<bool>(errors.FirstOrDefault()!));
        }

        base.OnActionExecuting(context);
    }
}