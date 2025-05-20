using Posta_Barnabas_Projekt.Data;
using Microsoft.EntityFrameworkCore;
using Posta_Barnabas_Projekt.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<K�nyvt�rAdatb�zis>(opci�k => 
opci�k.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IK�nyvService, K�nyvService>();
builder.Services.AddScoped<IOlvas�Service, Olvas�Service>();
builder.Services.AddScoped<IK�lcs�nz�sService, K�lcs�nz�sService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
