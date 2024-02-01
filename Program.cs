using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpicyX.DAL;
using SpicyX.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("default")));
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength= 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts= 3;
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromSeconds(30);
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = $"/Admin/Account/Login/{cfg.ReturnUrlParameter}";
});

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{area=exists}/{controller=home}/{action=index}/{id?}");

app.MapControllerRoute(
    "default",
    "{controller=home}/{action=index}/{id?}");



app.Run();
