using Microsoft.EntityFrameworkCore;
using WebApp.Dal;
using WebApp.Dal.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebAppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        x=>
        { 
            x.MigrationsAssembly("WebApp.Dal");
        });
});
builder.Services.AddTransient<IDataBaseSeeder, BaseDataBaseSeeder>();



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//intialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<IDataBaseSeeder>();
    context.Seed();
}

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