using Microsoft.AspNetCore.Identity;

public static class RolesInitializer
{
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        string[] roleNames = { "Admin", "User" };

        foreach (string roleName in roleNames)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                IdentityRole role = new IdentityRole { Name = roleName };
                await roleManager.CreateAsync(role);
            }
        }

        if (await userManager.FindByNameAsync("admin@teste.com") == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = "admin@teste.com",
                Email = "admin@teste.com"
            };

            IdentityResult result = await userManager.CreateAsync(adminUser, "SenhaSecreta@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}