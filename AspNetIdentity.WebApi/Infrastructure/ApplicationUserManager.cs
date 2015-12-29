using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AspNetIdentity.WebApi.Infrastructure
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<ApplicationDbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

            // Configure validation logic for usernames
            appUserManager.UserValidator = new UserValidator<ApplicationUser>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            
            appUserManager.EmailService = new AspNetIdentity.WebApi.Services.EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }
           
            return appUserManager;
        }

        public override async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return await Task.Run(() => new ApplicationUser
                {
                    UserName = userName,
                    Id = userName,
                    EmailConfirmed = true
                });
        }

        public override async Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            IList<Claim> claimCollection = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Country, "Canada"),
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claimCollection, "Test");

            return await Task.Run(() => claimsIdentity); 
        }

        public override IQueryable<ApplicationUser> Users
        {
            get
            {
                IList<ApplicationUser> userCollection = new List<ApplicationUser>
                {
                    new ApplicationUser{UserName = "Bob", Id = "Bob", EmailConfirmed = true},
                    new ApplicationUser{UserName = "Dick", Id = "Dick", EmailConfirmed = true},
                    new ApplicationUser{UserName = "Brenda", Id = "Brenda", EmailConfirmed = true}
                };

                return userCollection.AsQueryable<ApplicationUser>();
            }
        }

        public override async Task<IList<string>> GetRolesAsync(string userId)
        {
            IList<string> roles = new List<string> {"User"};

            //return await Task.Run(() => roles);

            return roles;
        }

        public override async Task<IList<Claim>> GetClaimsAsync(string userId)
        {
            IList<Claim> claims = new List<Claim> {new Claim(ClaimTypes.Role, "User") };

            //return await Task.Run(() => claims);

            return claims;
        }
    }
}