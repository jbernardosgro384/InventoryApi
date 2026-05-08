using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using InventoryApi.API.Middleware;
using InventoryApi.Application.Interfaces;
using InventoryApi.Application.Services;
using InventoryApi.Domain.Entities;
using InventoryApi.Infrastructure.Data;
using InventoryApi.Shared.Mappings;
using InventoryApi.Shared.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductService>();

builder.Host.UseSerilog();

builder.Services.AddSingleton<IMapper>(provider =>
{
    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<ProductProfile>();
    }, loggerFactory);

    return config.CreateMapper();
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    context.Database.Migrate();

    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product
            {
                Name = "Keyboard",
                Price = 100,
                Stock = 10
            },
            new Product
            {
                Name = "Mouse",
                Price = 50,
                Stock = 20
            });

        context.SaveChanges();
    }
}






app.UseMiddleware<ExceptionMiddleware>();
app.UseExceptionHandler("/error");

app.Map("/error", () =>
{
    return Results.Problem("An unexpected error occurred.");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();