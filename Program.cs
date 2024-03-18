using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SallesWebApp.Data;
using SallesWebApp.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SallesWebAppContext");
builder.Services.AddDbContext<SallesWebAppContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));
});

// Adicione o serviço SeedingService
builder.Services.AddTransient<SeedingService>();
builder.Services.AddTransient<SellerService>();
builder.Services.AddTransient<DepartmentService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

var enUs = new CultureInfo("en-US");
var localeOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUs),
    SupportedCultures = new List<CultureInfo> { enUs },
    SupportedUICultures = new List<CultureInfo> { enUs }
};

app.UseRequestLocalization(localeOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
