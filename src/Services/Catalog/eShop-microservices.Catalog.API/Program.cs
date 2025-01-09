using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;
using eShop_microservices.Catalog.API.Data;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opt => {
    opt.Connection((builder.Configuration.GetConnectionString("Postgresql"))!);
}).UseLightweightSessions();

if(builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(opt => { });
#region lambda exceptionHandler
//exceptionhandler with lambda
//app.UseExceptionHandler(exceptionHandler => {

//    exceptionHandler.Run(async context => {

//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if (exception is null) return;

//        var problemDetails = new ProblemDetails {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace  
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";
//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });

//});

#endregion
app.Run();

