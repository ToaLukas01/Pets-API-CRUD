using Microsoft.EntityFrameworkCore;
using PetsApiBackend.Models;
using PetsApiBackend.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AGREGAMOS EL CONTEXTO
const string ConectionName = "PetsApiDB";
var connectionString = builder.Configuration.GetConnectionString(ConectionName);

builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


// AUTOMAPPER
builder.Services.AddAutoMapper(typeof(Program));

// ADD SERVICE
builder.Services.AddScoped<IPetRepository, PetRepository>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder => // "AllowWebApp"
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
