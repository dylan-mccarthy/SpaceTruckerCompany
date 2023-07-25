using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SpaceTruckerCompany.Web.Admin.Service
{
    public class AuthUtils
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public AuthUtils(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task CreateRoleClaim(string roleName)
        {
            var claim = new Claim(ClaimTypes.Role, roleName);
            var role = await RoleManager.FindByNameAsync(roleName);
            var result = await RoleManager.AddClaimAsync(role, claim);
        }
        public async Task CreateRole(string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await RoleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return;
            }
        }

        public async Task AssignClaim(string userId, string claimName)
        {

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                Console.WriteLine("unable to find user");
                return;
            }
            //Check if user already has claim
            var userRoles = await UserManager.GetClaimsAsync(user);
            foreach (var role in userRoles)
            {
                if (role.Value == claimName)
                {
                    Console.WriteLine("User already has role");
                    return;
                }
            }
            var claim = new Claim(ClaimTypes.Role, claimName);
            var result = await UserManager.AddClaimAsync(user, claim);
            if (result.Succeeded)
            {
                return;
            }
        }
    }
}
