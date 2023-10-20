
using EticaretApp.Application.Validators.Products;
using EticaretApp.Infsrastructure.NewFolder;
using EticaretApp.Infsrastructure.Services2.Storage.Local;
using EticaretApp.Persistence;
using EticaretApp.Infsrastructure;
using FluentValidation.AspNetCore;
using EticaretApp.Infsrastructure.Services2.Storage.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();

//builder.Services.AddStorage(StorageType.Azure);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:57702", "http://localhost:56968/", "http://localhost:59981/").AllowAnyHeader().AllowAnyMethod()
));

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilters>()).AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
