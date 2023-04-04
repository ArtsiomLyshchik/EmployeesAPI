using EmployeesAPI.Data.Context;
using EmployeesAPI.RestAPI.Application.Behaviors;
using EmployeesAPI.RestAPI.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

    builder.Host
        .UseSerilog((context, _, loggerConfiguration) =>    
            loggerConfiguration
                .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code))
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)        
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()        
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .ReadFrom
                .Configuration(context.Configuration));

var connectionString = builder.Configuration.GetConnectionString("EmployeeDb");

builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseMySQL(connectionString,
    optionsBuilder => optionsBuilder.MigrationsAssembly("EmployeesAPI.RestAPI")));

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("x-api-version"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseMiddleware<HttpErrorMiddleware>();

app.Run();