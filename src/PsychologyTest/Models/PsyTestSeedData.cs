using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PsychologyTest.Models
{
    public class PsyTestSeedData
    {
        private PsyTestContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<PsyTestUser> _userManager;

        public PsyTestSeedData(PsyTestContext context, 
                               UserManager<PsyTestUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }


        public async Task SeedDataTask()
        {
            // Rol de ROOT
            if (! await _roleManager.RoleExistsAsync("Root")) {
                var roleRoot = new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole();
                roleRoot.Name = "Root";
                await _roleManager.CreateAsync(roleRoot);
            }

            // Rol de Administrador
            if (!await _roleManager.RoleExistsAsync("Admin")) {
                var roleAdmin = new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole();
                roleAdmin.Name = "Admin";
                await _roleManager.CreateAsync(roleAdmin);
            }

            // Rol de Psicologo
            if (!await _roleManager.RoleExistsAsync("Psy")) {
                var rolePsy = new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole();
                rolePsy.Name = "Psy";
                await _roleManager.CreateAsync(rolePsy);
            }

            // Rol de Estudiante
            if (!await _roleManager.RoleExistsAsync("Stud")) {
                var roleStud = new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole();
                roleStud.Name = "Stud";
                await _roleManager.CreateAsync(roleStud);
            }

            // Usuarios Root y SysAdmin
            if (await _userManager.FindByEmailAsync("su@root.com") == null) {
                var userRoot = new PsyTestUser() {
                    UserName = "su@root.com",
                    Email = "su@root.com",
                    EmailConfirmed = true
                };
                var resultRoot = await _userManager.CreateAsync(userRoot, "Root123456");
                if (resultRoot.Succeeded) {
                    await _userManager.AddToRoleAsync(userRoot,"Root");
                    await _userManager.AddClaimAsync(userRoot, new Claim("emailconfirmation", "1"));
                }
            }

            if (await _userManager.FindByEmailAsync("sys@admin.com") == null) {
                var userAdmin = new PsyTestUser() {
                    UserName = "sys@admin.com",
                    Email = "sys@admin.com",
                    EmailConfirmed = true
                };
                var resultAdmin = await _userManager.CreateAsync(userAdmin, "Admin123456");
                if (resultAdmin.Succeeded) {
                    await _userManager.AddToRoleAsync(userAdmin, "Admin");
                    await _userManager.AddClaimAsync(userAdmin, new Claim("emailconfirmation", "1"));
                }
            }

            //TODO: Poblar instituciones y grupos

            await _context.SaveChangesAsync();
        }
    }
}
