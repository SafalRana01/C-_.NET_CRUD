
using FirstCrudProject.DAL;
using FirstCrudProject.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Add dependency injection for Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Configure the DbContext to use SQL Server with the connection string from configuration
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// registering the services this is need to registered for every different employeeServices class.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


// Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging.AddSerilog();

builder.Services.AddSwaggerGen();


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
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
