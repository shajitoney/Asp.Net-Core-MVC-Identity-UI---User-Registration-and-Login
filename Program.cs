using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(); // Add this line to enable Blazor
builder.Services.Configure<IdentityOptions>(options =>
{

    options.Password.RequireUppercase = false;



});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Use MapRazorPages() instead
app.MapBlazorHub();  // Map the Blazor Hub

app.Run();

