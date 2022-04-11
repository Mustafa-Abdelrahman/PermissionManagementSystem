using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.Web.Constants;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.Seeds;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();
// policy-based authorization
builder.Services.AddAuthorization(options =>
{
  
    options.AddPolicy("Page1Access",
        policy => policy.RequireClaim(ClaimTypes.Webpage, Pages.Page1.ToString())
                        .RequireRole(Roles.Member.ToString()));

    options.AddPolicy("Page2Access",
policy => policy.RequireClaim(ClaimTypes.Webpage, Pages.Page2.ToString())
                        .RequireRole(Roles.Member.ToString()));

});
var app = builder.Build();


using (var scope  = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerProvider = services.GetRequiredService<ILoggerProvider>();
    var logger = loggerProvider.CreateLogger("app");

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var dbContext = services.GetRequiredService<ApplicationDbContext>();

        await SeedRoles.SeedRolesAsync(roleManager);
        logger.Log(LogLevel.Information, "Roles Seeding Completed.");

        await SeedUsers.SeedAdminAsync(userManager);
        await SeedUsers.SeedMemberAsync(userManager);
        logger.Log(LogLevel.Information, "Users Seeding Completed.");

    }
    catch (Exception ex)
    {
        logger.Log(LogLevel.Error, "Something went wrong while seeding DB!");

    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
