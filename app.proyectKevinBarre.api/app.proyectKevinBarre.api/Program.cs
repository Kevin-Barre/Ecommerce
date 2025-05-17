using app.projectKevinBarre.common.EventMQ;
using app.projectKevinBarre.services.eventMQ;
using app.projectKevinBarre.services.EventMQ;
using app.proyectKevinBarre.accessData.Context;
using app.proyectKevinBarre.accessData.repositories;
using app.proyectKevinBarre.services.Implementations;
using app.proyectKevinBarre.services.Interfaces;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("rabbitmq"));

// Obtener cadena de conexión 

var conSqlCoconecction = builder.Configuration.GetConnectionString("BDDSqlServer")!; 
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conSqlCoconecction);
    options.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
});

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();

builder.Services.AddScoped<IVentaDetalleService, VentaDetalleService>();
builder.Services.AddScoped<IVentaDetalleRepository, VentaDetalleRepository>();

builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();

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
