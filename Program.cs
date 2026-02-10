using B2BManagement.Constant;
using B2BManagement.Data;
using B2BManagement.Middleware;
using B2BManagement.Repository.Interfaces;
using B2BManagement.Repository;
using B2BManagement.Services.Interfaces;
using B2BManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using B2BManagement.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Repository DI
builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// Service DI
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddHttpClient();

var jwtKey = builder.Configuration["Jwt:Key"] ?? AppConstant.DefaultSignInKey;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerWithJwt();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
