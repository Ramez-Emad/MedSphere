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
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
