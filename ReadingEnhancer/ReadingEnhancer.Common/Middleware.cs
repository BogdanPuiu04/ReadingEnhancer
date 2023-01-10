using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using ReadingEnhancer.Common.CustomExceptions;

namespace ReadingEnhancer.Common;

public class Middleware
{
    private readonly RequestDelegate _next;

    public Middleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var isInternalServerError = false;
            switch (error)
            {
                case BadRequestException:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                case ResultNotFoundException:
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;
                case ConflictException:
                    response.StatusCode = (int) HttpStatusCode.Conflict;
                    break;
                case UnauthorizedException:
                    response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    break;
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    isInternalServerError = true;
                    break;
            }

            var responseModel = AppResponse<string>.Fail(GetErrorMessages(error, isInternalServerError));

            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }

    private static List<string> GetErrorMessages(Exception error, bool isInternalServerError)
    {
        var exceptionsList = new List<string>();

        if (error.GetType() == typeof(AggregateException))
        {
            var listOfInnerExceptions = ((AggregateException) error).InnerExceptions;
            exceptionsList.AddRange(listOfInnerExceptions.Select(err => err.Message));
        }
        else
        {
            exceptionsList.Add(!isInternalServerError
                ? error.Message
                : ValidationMessages.InternalError);
        }

        return exceptionsList;
    }
}