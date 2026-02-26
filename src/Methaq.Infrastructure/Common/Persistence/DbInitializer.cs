using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.ApplicationUsers.enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Methaq.Infrastructure.Common.Persistence;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var superAdminSettings = services.GetRequiredService<IOptions<SuperAdminSettings>>().Value;

            await context.Database.MigrateAsync();

            await SeedRolesAsync(roleManager);
            await SeedSuperAdminAsync(userManager, superAdminSettings);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["SuperAdmin", "CenterManager", "Supervisor", "Student"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    private static async Task SeedSuperAdminAsync(
        UserManager<ApplicationUser> userManager,
        SuperAdminSettings settings)
    {
        var existing = await userManager.FindByEmailAsync(settings.Email);
        if (existing is not null)
            return;

        var user = new ApplicationUser
        {
            FirstName = "Super",
            SecondName = "Admin",
            ThirdName = "Admin",
            LastName = "Admin",
            Email = settings.Email,
            UserName = settings.Email,
            EmailConfirmed = true,
            AccountStatus = AccountStatus.Approved
        };

        await userManager.CreateAsync(user, settings.Password);
        await userManager.AddToRoleAsync(user, "SuperAdmin");
    }
}
