using app.FacturaSubscribe.DataAccess.Context;
using app.FacturaSubscribe.services.Config;
using app.FacturaSubscribe.services.Implementacion;
using app.FacturaSubscribe.services.Interfaces;
using app.FacturaSubscribe.services.MQ;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Leer la configuración de RabbitMQ desde appsettings.json
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("rabbitmq"));

// 🔌 Configurar la cadena de conexión a PostgreSQL desde appsettings.json
var conPostgres = builder.Configuration.GetConnectionString("BDDPostgres")!;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(conPostgres);
});

// 🔧 Registrar el servicio de categoría con su interfaz
builder.Services.AddScoped<IProcesoService, ProcesoService>();



// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Background Service para RabbitMQ
builder.Services.AddHostedService<RabbitMqConsumerService>();


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
