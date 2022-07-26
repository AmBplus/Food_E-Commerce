using Domain.Models;
using F_e_commerce_EFCore;
using F_e_commerce_EFCore.IUnitOfWorks;
using F_e_commerce_EFCore.Repository.CategoryRepository;
using F_e_commerce_EFCore.Repository.FoodRepository;
using F_e_commerce_EFCore.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDbContext<FECommerceContext>
(op => op.UseSqlServer(connection, 
    b => b.MigrationsAssembly("F-e-commerce_EFCore")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<IEmailSender, CommonUtility.EmailSender>();
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddDefaultTokenProviders()
    .AddEntityFrameworkStores<FECommerceContext>();
builder.Services.AddTransient<IUnitOfWorkEF, UnitOfWorkEF>();
builder.Services.ConfigureApplicationCookie(op =>
{
    op.LoginPath = "/Identity/Account/Login";
    op.AccessDeniedPath = "/Identity/Account/Logout";
    op.LogoutPath = "/Identity/Account/AccessDenied";

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();
