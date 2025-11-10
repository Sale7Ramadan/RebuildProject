using BusinceLayer; // لازم حتى تستخدم AddBusinessLayer()
using DataAccessLayer; // لازم حتى تستخدم AddDataAccessLayer()
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;    // Optionally add for Swagger types
using Swashbuckle.AspNetCore.SwaggerGen; // Add this using directive
using Swashbuckle.AspNetCore.SwaggerUI; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// ✅ استخدم Swagger بدل AddOpenApi()
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataAccessLayer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddBusinessLayer();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // ✅ استخدم Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
