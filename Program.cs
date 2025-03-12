using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchoolApp.Data;
using SmithSwimmingSchoolApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy =>
        policy.RequireRole("Administrator")); 
    options.AddPolicy("Coach", policy =>
       policy.RequireRole("Coach")); 
    options.AddPolicy("Swimmer", policy =>
       policy.RequireRole("Swimmer")); 
    options.AddPolicy("Visitor", policy =>
    {
        policy.RequireAssertion(context =>
            !context.User.Identity.IsAuthenticated ||
            context.User.IsInRole("Visitor"));
    }
      /* policy.RequireRole("Visitor")*/); 
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Habilitar roles
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Habilitar autenticaci�n
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Aplicar migraciones y datos semilla autom�ticamente al iniciar la aplicaci�n
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Aplica migraciones
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations.");
    }
}

app.Run();
