using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revoo.Application.Interfaces;
using Revoo.Application.Services;
using Revoo.Infrastructure.Data;
using Revoo.Infrastructure.Repositories;
using Revoo.Domain.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.InvalidModelStateResponseFactory = ctx =>
    {
        var pd = new ValidationProblemDetails(ctx.ModelState)
        {
            Title = "Erro de validação",
            Status = StatusCodes.Status400BadRequest
        };
        return new BadRequestObjectResult(pd);
    };
});

builder.Services.AddDbContext<RevooDbContext>(opt =>
{
    opt.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IHabitoRepository, HabitoRepository>();
builder.Services.AddScoped<IMetaSemanalRepository, MetaSemanalRepository>();
builder.Services.AddScoped<IRegistroProgressoRepository, RegistroProgressoRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<HabitoService>();
builder.Services.AddScoped<MetaSemanalService>();
builder.Services.AddScoped<RegistroProgressoService>();

var app = builder.Build();

app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var ex = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        var (status, title) = ex switch
        {
            DomainException => (400, ex.Message),
            KeyNotFoundException => (404, ex.Message),
            _ => (500, "Erro inesperado.")
        };

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = title,
            Status = status,
            Detail = ex?.ToString()
        });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();