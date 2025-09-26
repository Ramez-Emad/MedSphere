using FluentValidation;
using Mapster;
using MapsterMapper;
using MedSphere.BLL;
using MedSphere.BLL.Mapping;
using MedSphere.BLL.Services.Ingredients;
using MedSphere.BLL.Services.Medicines;
using MedSphere.DAL.Data;
using MedSphere.DAL.Repositories.Ingredients;
using MedSphere.DAL.Repositories.MedicineIngredients;
using MedSphere.DAL.Repositories.Medicines;
using MedSphere.PL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region DbContext

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

#endregion

#region Repositories & Services
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IMedicineService, MedicineService>(); 

builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IIngredientService, IngredientService>();

builder.Services.AddScoped<IMedicineIngredientRepository, MedicineIngredientRepository>();
#endregion

#region Mapper Service

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(MappingConfig).Assembly);

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

#endregion

#region Validator Service

builder.Services.AddValidatorsFromAssemblyContaining<FluentValidationAssemblyReference>();
builder.Services.AddFluentValidationAutoValidation();
#endregion

#region CORS

builder.Services.AddCors(options =>
          options.AddDefaultPolicy(br =>
              br
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!)
          )
      );

#endregion

#region Exception Handling

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

#endregion

#region Logging

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseCors();

app.MapControllers();

app.MapFallback(async context =>
{
    context.Response.StatusCode = StatusCodes.Status404NotFound;
    await context.Response.WriteAsJsonAsync(new ProblemDetails
    {
        Status = StatusCodes.Status404NotFound,
        Title = "Not Found",
        Detail = "The requested endpoint does not exist."
    });
});

app.Run();
