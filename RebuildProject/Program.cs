using BusinceLayer; // لازم حتى تستخدم AddBusinessLayer()
using DataAccessLayer; // لازم حتى تستخدم AddDataAccessLayer()
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;    // Optionally add for Swagger types
using Swashbuckle.AspNetCore.SwaggerGen; // Add this using directive
using Swashbuckle.AspNetCore.SwaggerUI; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// ✅ استخدم Swagger بدل AddOpenApi()
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter JWT with Bearer prefix",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDataAccessLayer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddBusinessLayer();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ✅ إضافة JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles();
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // ✅ استخدم Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ إضافة Authentication قبل Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
