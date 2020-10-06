using AutoMapper;
using GameStoreBLL.Services.Abstraction;
using GameStoreClient.Helpers;
using GameStoreClient.Models;
using GameStoreClient.Utilits;
using GameStoreDAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IGameService gameService;
        private readonly IAddressService addressService;
        private readonly IMapper mapper;
        public OrderController(IOrderService _orderService,IGameService _gameService,IAddressService _addressService ,IMapper _mapper)
        {
            orderService = _orderService;
            gameService = _gameService;
            addressService = _addressService;
            mapper = _mapper;
        }
        public ActionResult Index()
        {
            List<Order> orders = new List<Order>();
            List<User> users = new List<User>();
            foreach (var item in orderService.GetAllOrders().ToList())
            {
                if (item.UserId == User.Identity.GetUserId())
                    orders.Add(item);
            }
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
           
            foreach (var item in orders)
            {
                users.Add(manager.FindById(item.UserId));
            }
            ViewBag.Users = users;
            return View(orders);
        }
        [HttpGet]
        public ActionResult CreateOrder()
        {
            var cart = Session["Cart"] as List<CartUnit>;
            
            Order order = new Order();
            foreach (var item in cart)
            {
                for (int i = 0; i < item.Amount; i++)
                {
                    order.Games.Add(gameService.Find(item.Game.Id));
                }
            }
            order.UserId = User.Identity.GetUserId();
            
            order.Date = DateTime.Today;
            orderService.AddOrder(order);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ShowOrder(int id)
        {

            var order = orderService.Find(id);
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            ViewBag.User = manager.FindById(order.UserId);
            int allAmount = 0;
            double allPrice = 0;
            List<CartUnit> games = new List<CartUnit>();
            foreach (var item in order.Games.GroupBy(x=>x.Name))
            {
                games.Add(new CartUnit()
                {
                    Game = mapper.Map<GameViewModel>(item.First()),
                    Amount = item.Count(),
                });
            }
            foreach (var item in games)
            {
                allAmount += item.Amount;
            }
            ViewBag.Games = games;
            foreach (var item in order.Games)
            {
                allPrice += item.Price;
               
            }
            ViewBag.AllPrice = allPrice;
            ViewBag.AllAmount = allAmount;
            return View(order);
        }
        [HttpGet]
        public ActionResult GetAllOrders()
        {
            List<Order> orders = orderService.GetAllOrders().ToList();
            List<User> users = new List<User>();
            
            var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            foreach (var item in orders)
            {
                users.Add(manager.FindById(item.UserId));
            }
            ViewBag.Users = users;
            return View(orders);
            
        }

        [HttpGet]
        public ActionResult DeleteOrder(int id)
        {
            orderService.Delete(orderService.Find(id));
            return RedirectToAction("GetAllOrders");
        }
        public ActionResult ProblemPage()
        {
            return View();
        }
    }
}