using AutoMapper;
using GameStoreBLL.Services.Abstraction;
using GameStoreClient.Models;
using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreClient.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;
        private readonly IMapper mapper;
        public GenreController(IGenreService _genreService, IMapper _mapper)
        {
           genreService = _genreService;
            mapper = _mapper;

        }
        public ActionResult Index()
        {
            var genres = genreService.GetAll();

            return View(mapper.Map<ICollection<GenreViewModel>>(genres));
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(GenreViewModel genre)
        {
            genreService.Create(mapper.Map<Genre>(genre));
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var genre = genreService.Find(id);
            if (!genreService.IsUse(genre))
            {
                genreService.Delete(genre);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}