using AutoMapper;
using GameStoreBLL.Services.Abstraction;
using GameStoreClient.Helpers;
using GameStoreClient.Models;
using GameStoreClient.Utilits;
using GameStoreDAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameStoreClient.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        private readonly IMapper mapper;
        private readonly IAddressService addressService;

        public AuthController(IMapper _mapper,IAddressService _addressService)
        { 
            mapper = _mapper;
            addressService = _addressService;
        }
        public ActionResult Login()
        {
            Session["Cart"] = new List<CartUnit>();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var sigininManager = HttpContext.GetOwinContext().Get<AppSigninMaanager>();

            var status = await sigininManager.PasswordSignInAsync(model.Login, model.Password, false, false);
            if (status == SignInStatus.Success)
                return RedirectToAction("Index", "Game");
            else return RedirectToAction("ProblemPage");


        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]  
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

                var user = new User
                {
                    UserName = model.Login,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber

                };
                string errors = "";
                var result = await manager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var item in result.Errors)
                    {
                        errors += item;
                    }
                    return Content(errors);
                }

            }
            else
            {
                return RedirectToAction("Register");
            }
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            
            return RedirectToAction("Index", "Game");
            
        }
        public ActionResult Profile()
        {
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var model = manager.FindByName(User.Identity.Name);


            return View(model);
        }
        [HttpGet]
        public ActionResult Address()
        {
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var model = manager.FindByName(User.Identity.Name).Address;

            return View(mapper.Map<AddressViewModel>(model));
        }

        [HttpGet]
        public ActionResult AddressFromOrder()
        {
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var model = manager.FindByName(User.Identity.Name).Address;

            return View(mapper.Map<AddressViewModel>(model));
        }
        [HttpPost]
        public ActionResult AddressFromOrder(AddressViewModel address)
        {
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var model = manager.FindByName(User.Identity.Name);

            addressService.Create(mapper.Map<Address>(address));
            var addressUser = addressService.FindByAddress(mapper.Map<Address>(address));
            model.AddressId = addressUser.Id;
            manager.Update(model);

            return RedirectToAction("MakeOrder");
        }
        [HttpPost]
        public ActionResult Address(AddressViewModel address)
        {
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var model = manager.FindByName(User.Identity.Name);
            
           addressService.Create( mapper.Map<Address>(address));
            var addressUser = addressService.FindByAddress(mapper.Map<Address>(address));
            model.AddressId = addressUser.Id;
            manager.Update(model);
            
            return RedirectToAction("Profile");
        }

        public ActionResult Cart()
        {
            var units = HttpContext.Session["Cart"];
            return View(units);
        }
        
        public ActionResult RaiseAmount(string name)
        {
            var units = Session["Cart"]as List<CartUnit>;
            foreach (var item in units)
            {
                if (item.Game.Name == name)
                    item.Amount += 1;
            }
            Session["Cart"] = units;
            return PartialView("CartUnits",units);
        }
        
        public ActionResult ReduceAmount(string name)
        {
            var units = Session["Cart"] as List<CartUnit>;
            foreach (var item in units)
            {
                if (item.Game.Name == name)
                    item.Amount -= 1;
            }
            Session["Cart"] = units;
            return PartialView("CartUnits",units);
        }
        public ActionResult DeleteCartUnit(string name)
        {
            var units = Session["Cart"] as List<CartUnit>;
            for(int i=0;i<units.Count;i++) 
            {
                if (units[i].Game.Name == name)
                    units.Remove(units[i]);
            }
            Session["Cart"] = units;
            return PartialView("CartUnits", units);
        }
        public ActionResult MakeOrder()
        {
            var units = Session["Cart"] as List<CartUnit>;
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var model = manager.FindByName(User.Identity.Name);
            ViewBag.User = model;
            int allAmount = 0;
            double allPrice = 0;
            foreach (var item in units)
            {
                allPrice += item.GetAllPrice();
                allAmount += item.Amount;
            }
            ViewBag.AllPrice = allPrice;
            ViewBag.AllAmount = allAmount;
            return View(units);
        }
        public ActionResult ProblemPage()
        {
            return View();
        }
    }
}