using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Projekt_zaliczeniowy.Data;

public class IdentityContext : IdentityDbContext<IdentityUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN".ToUpper() }, 
            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER".ToUpper() }
        );

        var hasher = new PasswordHasher<IdentityUser>();

        builder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "ef393d67-82d7-4049-8015-2a1b24a90c69", 
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed=true,
                PasswordHash = hasher.HashPassword(null, "Test123!")
            },
            new IdentityUser
            {
                Id = "9b2bbed4-4753-445c-b47e-4d0eaa925455", 
                UserName = "nowak@gmail.com",
                NormalizedUserName = "NOWAK@GMAIL.COM",
                Email = "nowak@gmail.com",
                NormalizedEmail = "NOWAK@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Test123!")
            },
            new IdentityUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", 
                UserName = "kowalski@gmail.com",
                NormalizedUserName = "KOWALSKI@GMAIL.COM",
                Email = "kowalski@gmail.com",
                NormalizedEmail = "KOWALSKI@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Test123!")
            }
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "ef393d67-82d7-4049-8015-2a1b24a90c69"
            },
            new IdentityUserRole<string>
            {
                RoleId = "2",
                UserId = "9b2bbed4-4753-445c-b47e-4d0eaa925455"
            },
            new IdentityUserRole<string>
            {
                RoleId = "2",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );


    }
}
