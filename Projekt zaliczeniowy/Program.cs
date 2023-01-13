using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Projekt_zaliczeniowy.Data;
using Projekt_zaliczeniowy.Models.Interfaces;
using Projekt_zaliczeniowy.Models.Services;

namespace Projekt_zaliczeniowy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(builder.Configuration["Data:Connection"]));

            builder.Services.AddScoped<ITeamService, TeamServiceEF>();
            builder.Services.AddScoped<IPlayerService, PlayerServiceEF>();
            builder.Services.AddScoped<IMatchService, MatchServiceEF>();
            builder.Services.AddScoped<ITicketService, TicketServiceEF>();

            builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(builder.Configuration["Data:Connection"]));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

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
                        app.UseAuthentication();;

            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Match}/{action=Index}/{id?}");

            app.Run();
        }
    }
}