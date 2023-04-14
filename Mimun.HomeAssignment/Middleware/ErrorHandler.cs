using System.Net;

namespace Mimun.HomeAssignment.Middleware
{
    public class ErrorHandler
    {
        readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //It is better to log the exception info for debugging purpose
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = ex switch
                {
                    ApplicationException _ => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException _ => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
