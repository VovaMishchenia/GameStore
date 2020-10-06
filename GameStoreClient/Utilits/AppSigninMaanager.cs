using GameStoreDAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreClient.Utilits
{
    public class AppSigninMaanager:SignInManager<User,string>
    {
        public AppSigninMaanager(AppUserManager userManager,IAuthenticationManager authenticationManager) : base(userManager,authenticationManager)
        {
            
        }
        public static AppSigninMaanager Create(IdentityFactoryOptions<AppSigninMaanager> options, IOwinContext owinContext)
        {
            var manager = owinContext.GetUserManager<AppUserManager>();
            var siginManager = new AppSigninMaanager(manager, owinContext.Authentication);

            return siginManager;
        }

    }
}