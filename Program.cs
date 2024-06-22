using Back_Gestion.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Ajout du service de session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Temps d'expiration de la session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Utilisation des sessions
app.UseSession();

// Middleware personnalisé pour rediriger vers la page de connexion si pas de token
app.Use(async (context, next) =>
{
    if (!context.Session.Keys.Contains("Token") && context.Request.Path != "/Login")
    {
        context.Response.Redirect("./Login");
    }
    else
    {
        await next();
    }
});

app.UseAuthorization();

app.MapRazorPages();

app.Run();
