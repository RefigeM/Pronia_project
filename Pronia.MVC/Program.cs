using Microsoft.EntityFrameworkCore;
using Pronia.BL.Services.Abstractions;
using Pronia.BL.Services.Concretes;
using Pronia.DAL.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Controller v? View-l?rin ?lav? edilm?si
builder.Services.AddControllersWithViews();

// Düzgün ConnectionString ?ld? edirik (MSSql istifad? edirik)
string? connectionStr = builder.Configuration.GetConnectionString("MSSql");

// DbContext-in konfiqurasiyas?
builder.Services.AddDbContext<ProniaDBContext>(opt =>
{
    if (string.IsNullOrEmpty(connectionStr))
    {
        throw new InvalidOperationException("Connection string 'MSSql' not found.");
    }
    opt.UseSqlServer(connectionStr);
});

// Service-l?rin ?lav? edilm?si
builder.Services.AddScoped<ISliderItemService, SliderItemService>();

var app = builder.Build();

// Controller route t?yin edirik
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
