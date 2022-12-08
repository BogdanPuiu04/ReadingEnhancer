using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReadingEnhancer.Common.Handlers;

public class ValidateDataToCustomResponse : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage)
            .ToList();

        var responseModel = AppResponse<string>.Fail(errors);

        context.Result = new JsonResult(responseModel)
        {
            StatusCode = 400
        };
    }
}