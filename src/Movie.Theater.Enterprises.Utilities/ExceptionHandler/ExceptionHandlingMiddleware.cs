using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Movie.Theater.Enterprises.Utilities.ExceptionHandler
{
    /// <summary>
    /// A class to handle exceptions throws and display proper json responses
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate requestDelegate;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        /// <summary>
        /// Catches exception and calls handleexception method
        /// </summary>
        /// <param name="context">holds information about the current HTTP request</param>
        /// <param name="logger">logger to log exceptions</param>
        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, logger);
            }
        }

        /// <summary>
        /// Handles thrown exception and assigns proper messages and codes 
        /// </summary>
        /// <param name="context">holds information about the current HTTP request</param>
        /// <param name="ex">exception to modify</param>
        /// <param name="logger">logger to log exceptions</param>
        /// <returns>modified http object response for exception</returns>
        private static Task HandleException(HttpContext context, Exception ex, ILogger<ExceptionHandlingMiddleware> logger)
        {
            var errorMessageObject = new Error { Timestamp = (DateTime.Now).ToString(), ErrorCode = Constants.INTERAL_SERVER_ERROR, ErrorMessage = ex.Message };
            var statusCode = (int)HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case ServiceUnavailableException:
                    errorMessageObject.ErrorCode = Constants.SERVER_UNAVAILABLE_ERROR;
                    statusCode = (int)HttpStatusCode.ServiceUnavailable;
                    break;
                case BadRequestException:
                    logger.LogError(ex.Message);
                    errorMessageObject.ErrorCode = Constants.BAD_REQUEST_ERROR;
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFoundException:
                    logger.LogError(ex.Message);
                    errorMessageObject.ErrorCode = Constants.NOT_FOUND_ERROR;
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ConflictException:
                    logger.LogError(ex.Message);
                    errorMessageObject.ErrorCode = Constants.CONFLICT_ERROR;
                    statusCode = (int)HttpStatusCode.Conflict;
                    break;
                case UnauthorizedException:
                    logger.LogError(ex.Message);
                    errorMessageObject.ErrorCode = Constants.UNAUTHORIZED_ERROR;
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;

            }

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(errorMessage);
        }
    }

}
