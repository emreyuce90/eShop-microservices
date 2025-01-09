using BuildingBlocks.Behaviors;
using Marten;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddMarten(opt => {
    opt.Connection((builder.Configuration.GetConnectionString("Postgresql"))!);
}).UseLightweightSessions();

var app = builder.Build();
app.MapCarter();
app.Run();

