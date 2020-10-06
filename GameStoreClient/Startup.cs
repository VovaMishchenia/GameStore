using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using GameStoreClient.Helpers;
using GameStoreClient.Utilits;
using GameStoreDAL;
using GameStoreDAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GameStoreClient.Startup))]

namespace GameStoreClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //повідомляємо Identity , який контекст використовувати
            app.CreatePerOwinContext<DbContext>(() => new ApplicationContext());

            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSigninMaanager>(AppSigninMaanager.Create);

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Login")
            });
            
            InitUsers();
        }

        private void InitUsers()
        {
            var userStore = new UserStore<User>(new ApplicationContext());
            var userManager = new UserManager<User>(userStore);
           

            var role = new IdentityRole
            {
                Name = "Admin"
            };
            var roleStore = new RoleStore<IdentityRole>(new ApplicationContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(role);
            
            var user = new User
            {
                UserName = "Vova",
                Email = "vova@gmail.com",
                PhoneNumber="+380123456789"
                
            };
            userManager.Create(user, "Qwerty1!");

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com"
            };
            userManager.Create(admin, "Qwerty1!");

            userManager.AddToRole(userManager.FindByName("admin").Id, "Admin");

        }
    }
}
