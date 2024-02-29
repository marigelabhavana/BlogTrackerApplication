using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogTrackerApplication.Data;
using BlogTrackerApplication.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogTrackerAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogTrackerAppDbContext") ?? throw new InvalidOperationException("Connection string 'BlogTrackerAppDbContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<BlogTrackerAppDbContext>();

    // Check if there are any existing admin records
    if (!dbContext.AdminInfo.Any())
    {
        // Create a new AdminInfo instance with email and password
        var admin = new AdminInfo
        {
            EmailId = "admin@example.com",
            Password = "adminpassword" // You should hash the password in a real application
        };

        // Add the admin record to the database
        dbContext.AdminInfo.Add(admin);
        dbContext.SaveChanges();
    }
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "adminLogin",
        pattern: "admin/login",
        defaults: new { controller = "AdminInfo", action = "Login" });

    endpoints.MapControllerRoute(
        name: "employeeLogin",
        pattern: "employee/login",
        defaults: new { controller = "EmpInfo", action = "EmployeeLogin" });

    endpoints.MapControllerRoute(
        name: "employeeLogout",
        pattern: "employee/logout",
        defaults: new { controller = "EmpInfo", action = "Logout" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=BlogInfoes}/{action=Index}/{id?}");
});

app.Run();