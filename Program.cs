using Microsoft.EntityFrameworkCore;
using ProvaPub.Common;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Services;
using ProvaPub.Repository;
using ProvaPub.Repository.Data;
using ProvaPub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<RandomService>();

// Configurar banco de dados usando ConfigeServiceDataBase
//var connectionString = builder.Configuration.GetConnectionString("ctx");

// Configuração do banco de dados
builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));

// Registre os serviços que dependem do contexto
builder.Services.AddTransient<IRandomRepository, RandomRepository>();
builder.Services.AddTransient<IRandomService, RandomService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

//ConfigureServiceDependencyInjection.ConfigureDependenciesService(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
