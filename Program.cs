using Flor.Data;
using Flor.Models;
using Flor.Services; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    context.Database.Migrate();

    if (!context.Buketi.Any())
    {
        context.Buketi.AddRange(
            new Buket
            {
                Naziv = "Romantični buket",
                Opis = "Idealno za posebne trenutke. Ruže, ljiljani i ljubav.",
                SlikaUrl = "/slike/buket1.jpg",
                CijenaMali = 20,
                CijenaSrednji = 40,
                CijenaVeliki = 60
            },
            new Buket
            {
                Naziv = "Proljetni mix",
                Opis = "Svježina proljeća u šarenom buketu punom boja.",
                SlikaUrl = "/slike/buket2.jpg",
                CijenaMali = 20,
                CijenaSrednji = 40,
                CijenaVeliki = 60
            },
            new Buket
            {
                Naziv = "Bijeli klasik",
                Opis = "Za sofisticirane prilike – čisto, jednostavno i elegantno.",
                SlikaUrl = "/slike/buket3.jpg",
                CijenaMali = 20,
                CijenaSrednji = 40,
                CijenaVeliki = 60
            }
        );
        context.SaveChanges();
    }

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    string adminEmail = "lea.admin@gmail.com";
    string adminPassword = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
