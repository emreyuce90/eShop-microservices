﻿

using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BuildingBlocks.Exceptions {
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {

            logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exception.Message, DateTime.UtcNow);


            (string Detail, string Title, int StatusCode) details = exception switch {
                InternalServerException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                NotFoundException => (

                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                ValidationException => (
                 exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException => (
                exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                _ =>
                (
                exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError

                )

            };

            var problemDetails = new ProblemDetails {
                Title = details.Title,
                Detail = details.Detail,
                Instance= httpContext.Request.Path,
                Status = details.StatusCode
            };


            problemDetails.Extensions.Add("traceId",httpContext.TraceIdentifier);

            if(exception is ValidationException validationException) {
                problemDetails.Extensions.Add("ValidationErrors",validationException.Errors);
            }
            await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken:cancellationToken);
            return true;

        }
    }
}
