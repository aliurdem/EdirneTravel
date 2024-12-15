using EdirneTravel.Application.Services;
using EdirneTravel.Application.Services.Base;
using EdirneTravel.Application.Services.ImageService;
using EdirneTravel.Data;
using EdirneTravel.Extensions;
using EdirneTravel.Models.Dtos.Auth;
using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);


builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));

// Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));
builder.Services.AddScoped<ITravelRouteService, TravelRouteService>();
builder.Services.AddScoped<IImageService, ImageService>();

// CORS Ayarlarý
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .SetIsOriginAllowed(origin =>
            {
                if (Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                {
                    return uri.Host == "localhost"; // Tüm localhost adreslerini kabul et
                }
                return false;
            })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var app = builder.Build();

// Identity minimal API'lerini map et
app.MapIdentityApi<User>();

// Kimlik doðrulama gerektiren bir logout endpointi örneði (SignInManager var)
app.MapPost("/logout", async (SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

app.MapGet("/me", async (UserManager<User> userManager, ClaimsPrincipal user) =>
{
    var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null)
    {
        return Results.NotFound(new { message = "Kullanýcý bulunamadý." });
    }

    var currentUser = await userManager.FindByIdAsync(userId);
    if (currentUser == null)
    {
        return Results.NotFound(new { message = "Kullanýcý bulunamadý." });
    }

    return Results.Ok(new
    {
        userId = currentUser.Id,
        email = currentUser.Email
    });
}).RequireAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
