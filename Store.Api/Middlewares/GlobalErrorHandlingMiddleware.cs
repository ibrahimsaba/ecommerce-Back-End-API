using Domain.Exeptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorsModels;

namespace Store.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate _next, ILogger<GlobalErrorHandlingMiddleware> _logger)
        {
            next = _next;
            logger = _logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetalis()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"End Point {context.Request.Path} is Not Found"
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ErrorDetalis()
                {
                    ErrorMessage = ex.Message
                };
                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                };
                context.Response.StatusCode = response.StatusCode;
                await  context.Response.WriteAsJsonAsync(response);

            }

        }
    }
}
