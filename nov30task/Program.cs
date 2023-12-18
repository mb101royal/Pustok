using Microsoft.EntityFrameworkCore;
using nov30task.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PustokDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
});

builder.Services.AddSession();

/*builder.Services.AddScoped<LayoutService>();
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "shop",
    pattern: "magaza",
    defaults: new
    {
        Controller = "Shop",
        Action = "Index"
    }
);

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Sliders}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
