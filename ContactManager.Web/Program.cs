using ContactManager.Core.Interfaces;
using ContactManager.Core.Services;
using ContactManager.Infrastructure.Data;
using ContactManager.Core.Interfaces;
using ContactManager.Core.Services;
using ContactManager.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
using System.Text;
using Ardalis.ListStartupServices;
using DTUP.Web.Infrastructure.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
        });
});

builder.Services.AddControllers();
ConfigureDevelopmentServices(builder.Services);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<DbSeederHostedService>();
builder.Services.AddControllersWithViews();
var app = builder.Build();
var con = builder.Configuration.GetConnectionString("CatalogConnection");




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Contact/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contact}/{action=Index}/{id?}");

app.Run();


void ConfigureDevelopmentServices(IServiceCollection services)
{
    // use real database
    // Requires LocalDB which can be installed with SQL Server Express 2016
    // https://www.microsoft.com/en-us/download/details.aspx?id=54284
    services.AddDbContext<ContactManagerContext>(c =>
        c.UseSqlServer(builder.Configuration.GetConnectionString("CatalogConnection")));
    //services.AddDbContext<ContactManagerContext>();

    // Add Identity DbContext




    ConfigureServices(services);
}
void ConfigureServices(IServiceCollection services)
{


    services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

    services.AddScoped<ILogService, LogService>();
    services.AddScoped<IContactService, ContactService>();



    services.Configure<ServiceConfig>(config =>
    {
        config.Services = new List<ServiceDescriptor>(services);

        config.Path = "/allservices";
    });


    // The TempData provider cookie is not essential. Make it essential
    // so TempData is functional when tracking is disabled.
    services.Configure<CookieTempDataProviderOptions>(options => {
        options.Cookie.IsEssential = true;
    });




    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest)

    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
    //_services = services; // used to debug registered services


}