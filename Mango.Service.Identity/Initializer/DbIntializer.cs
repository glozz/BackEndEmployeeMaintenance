using IdentityModel;
using Mango.Service.Identity.DbContexts;
using Mango.Service.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Service.Identity.Initializer
{
    public class DbIntializer : IDbIntializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbIntializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            if (_roleManager.FindByIdAsync(StaticDetails.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Customer)).GetAwaiter().GetResult();
            }
            else { return; }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111111",
                FirstName = "Gloucester",
                LastName = "Mafokoane"
            };
            _userManager.CreateAsync(adminUser, "Admin@123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, StaticDetails.Admin).GetAwaiter().GetResult();

          var temp1  = _userManager.AddClaimsAsync(adminUser,
                new Claim[] {
                     new Claim(JwtClaimTypes.Name,adminUser.FirstName+ " "+ adminUser.LastName),
                     new Claim(JwtClaimTypes.GivenName,adminUser.FirstName),
                     new Claim(JwtClaimTypes.FamilyName,adminUser.LastName),
                     new Claim(JwtClaimTypes.Role, StaticDetails.Admin)
            }).Result;

            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "customer1@gmail.com",
                Email = "customer1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111100",
                FirstName = "Peter",
                LastName = "Monyama"
            };
            _userManager.CreateAsync(customerUser, "Admin@123").GetAwaiter().GetResult();

            try
            {
                _userManager.AddToRoleAsync(customerUser, StaticDetails.Customer).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

                throw;
            }

            var temp2 = _userManager.AddClaimsAsync(customerUser,
                  new Claim[] {
                     new Claim(JwtClaimTypes.Name,customerUser.FirstName+ " "+ customerUser.LastName),
                     new Claim(JwtClaimTypes.GivenName,customerUser.FirstName),
                     new Claim(JwtClaimTypes.FamilyName,customerUser.LastName),
                     new Claim(JwtClaimTypes.Role, StaticDetails.Customer)
              }).Result;
        }
    }
}
