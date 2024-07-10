using Microsoft.EntityFrameworkCore;
using SillyGardening.Core.Abstract;
using SillyGardening.Core.GameComponents;
using SillyGardening.Core.Services;
using SillyGardening.DAL;
using SillyGardening.DAL.Abstract;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<Context>(options =>
                                         options.UseSqlServer(builder.Configuration
                                                                     .GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton<IGameSession, GameSession>();



// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Game}/{action=Game}/{id?}");

app.Run();
