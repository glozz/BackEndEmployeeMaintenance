using Mango.Web;
using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IProductServices, ProductService>();

SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
//builder.Configuration.GetSection("ServiceUrls").GetSection("ProductAPI").Value;
builder.Services.AddScoped<IProductServices,ProductService>();
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
    pattern: "{controller=Product}/{action=ProductIndex}/{id?}");

app.Run();
