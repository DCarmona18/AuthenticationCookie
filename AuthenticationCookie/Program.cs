using AuthenticationCookie.Domain.Events;
using AuthenticationCookie.Domain.Interfaces;
using AuthenticationCookie.Domain.Services;
using AuthenticationCookie.Infrastructure.Context;
using AuthenticationCookie.Infrastructure.Context.Entities;
using AuthenticationCookie.Infrastructure.Interfaces;
using AuthenticationCookie.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>
        (o => o.UseInMemoryDatabase("MyDatabase"));

// Add Service to use Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => 
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        //options.SlidingExpiration = true;
        //options.AccessDeniedPath = "/Forbidden/";
        options.EventsType = typeof(CustomCookieAuthenticationEvents); // Cookie middleware / guard for every request
    });
#region Add logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
#endregion

#region DI
// Service for Events
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

var app = builder.Build();

#region Data seed
var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<AppDbContext>();

// Data seed
var users = new List<User>
        {
            new User
            {
                Id= 1,
                Email = "dcarmona",
                Password = "123456",
                Name = "Daniel Carmona",
                Role = "Admin",
                LastChanged = new DateTime(2022, 7, 18).ToLongDateString()
            },
            new User
            {
                Id= 2,
                Email = "andere",
                Password = "123456",
                Name = "Jemanden",
                Role = "Verkaufer",
                LastChanged = new DateTime(2022, 12, 13).ToLongDateString()
            }
        };

db!.Users.AddRange(users);
await db.SaveChangesAsync();
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
