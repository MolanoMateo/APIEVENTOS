using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIEVENTOS.Data;
using APIEVENTOS.Models;
using System.Text;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<APIEVENTOSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIEVENTOSContext") ?? throw new InvalidOperationException("Connection string 'APIEVENTOSContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("APIEVENTOSContext"));
}
);
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
