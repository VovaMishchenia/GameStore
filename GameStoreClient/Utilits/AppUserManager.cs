using GameStoreDAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace GameStoreClient.Utilits
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store) : base(store)
        {
        }
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext owinContext)
        {
            var dbContext = owinContext.Get<DbContext>();
            var manager = new AppUserManager(new UserStore<User>(dbContext));

            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength=6,
                RequireDigit=true,
                RequireLowercase=true,
                RequireUppercase=true,
                RequireNonLetterOrDigit=true
            };

            var dataProvider = options.DataProtectionProvider;
            if (dataProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User> (dataProvider.Create("token"));
            }

            return manager;
        }
    }
}