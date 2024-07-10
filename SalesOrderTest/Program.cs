using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesOrder.Data.Interfaces;
using SalesOrder.Data.Models;
using SalesOrder.Services.Repositories;
using SalesOrder.Services.Services;
using SalesOrderTest.DBSource;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IDataSource>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new DataSource(connectionString);
});


// Register IXmlFileHandler and its implementation
builder.Services.AddScoped<IXmlFileHandler, XmlFileHandlerRepository>();

// Register ISalesOrderRepository with its concrete implementation
builder.Services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();


builder.Services.AddScoped<OrderHeaderService>();
builder.Services.AddScoped<IRepository<OrderLine>, OrderLineRepository>();
builder.Services.AddScoped<OrderHeaderService>();

builder.Services.AddScoped<IRepository<OrderHeader>, OrderHeaderRepository>();

var app = builder.Build();

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
