using SunriseSunsetTracker.Web.Interfaces;
using SunriseSunsetTracker.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ICityService, CityService>(client => 
{
    client.BaseAddress = new Uri(builder.Configuration["HttpClients:ServerCityClient:BaseUrl"]!);
});

builder.Services.AddHttpClient<IEventTimeService, EventTimeService>(client => 
{
    client.BaseAddress = new Uri(builder.Configuration["HttpClients:ServerEventTimeClient:BaseUrl"]!);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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