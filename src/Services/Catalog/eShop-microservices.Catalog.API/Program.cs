using BuildingBlocks.Behaviors;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddMarten(opt => {
    opt.Connection((builder.Configuration.GetConnectionString("Postgresql"))!);
}).UseLightweightSessions();

var app = builder.Build();
app.MapCarter();

app.UseExceptionHandler(exceptionHandler => {

    exceptionHandler.Run(async context => {

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null) return;

        var problemDetails = new ProblemDetails {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace  
        };

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, exception.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    });

});


app.Run();

