using AutoMapper;
using GameStoreBLL.Services.Abstraction;
using GameStoreClient.Models;
using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreClient.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IDeveloperService developerService;
        private readonly IMapper mapper;
        public DeveloperController(IDeveloperService _devService, IMapper _mapper)
        {
            developerService = _devService;
            mapper = _mapper;

        }
        // GET: Developer
        public ActionResult Index()
        {
            var devs = developerService.GetAll();
            return View(mapper.Map<ICollection<DeveloperViewModel>>(devs));
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(DeveloperViewModel dev)
        {
            developerService.Create(mapper.Map<Developer>(dev));
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dev = developerService.Find(id);
            if (!developerService.IsUse(dev))
            {
                developerService.Delete(dev);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}