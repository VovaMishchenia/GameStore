using AutoMapper;
using GameStoreBLL;
using GameStoreBLL.Services.Abstraction;
using GameStoreClient.Helpers;
using GameStoreClient.Models;
using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GameStoreClient.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;

        public GameController(IGameService _gameService, IMapper _mapper)
        {
         
            gameService = _gameService;
            mapper = _mapper;

        }
        // GET: Game
        public ActionResult Index()
        {
            ViewBag.Genres = gameService.GetGenres();
            ViewBag.Developers = gameService.GetAllDevelopers();
            ViewBag.Images = gameService.GetImages();

            //Session["Cart"] = new List<CartUnit>();
            var games = gameService.GetAllGames();
            var models = mapper.Map<ICollection<GameViewModel>>(games);

            return View(models);
        }
        [HttpGet]
        public ActionResult Filter(string type, string name)
        {
            if (type != null && name != null)
                AddFilter(type, name);
            var games = gameService.GetAllGames(Session["GamesFilter"] as List<GameFilter>);
            var models = mapper.Map<ICollection<GameViewModel>>(games);
            return PartialView("Cards", models);
        }

        [HttpGet]
        public ActionResult OrderByLow()
        {
            var filters = Session["GamesFilter"] as List<GameFilter>;
            return PartialView("Cards", mapper.Map<ICollection<GameViewModel>>(gameService.OrderByLow(filters)));
        }
        [HttpGet]
        public ActionResult OrderByHeigh()
        {
            var filters = Session["GamesFilter"] as List<GameFilter>;
            return PartialView("Cards", mapper.Map<ICollection<GameViewModel>>(gameService.OrderByHeigh(filters)));
        }
        [HttpGet]
        public ActionResult Search(string name)
        {
            var games = gameService.FindByName(name);
            return PartialView("Cards", mapper.Map<ICollection<GameViewModel>>(games));
        }
        private void AddFilter(string type, string name)
        {
            var filters = Session["GamesFilter"] as List<GameFilter>;
            // if filters don't exist
            if (filters == null)
                filters = new List<GameFilter>();
            var f = filters.FirstOrDefault(x => x.Type == type && x.Name == name);
            if (f != null)
            {
                filters.Remove(f);
                Session["GamesFilter"] = filters;
                return;
            }
            //new filter
            var filter = new GameFilter()
            {
                Name = name,
                Type = type,

            };
            //set predicates
            if (type == "Genre")
            {
                filter.Predicate = (x => x.Genre.Name == name);
            }
            else if (type == "Developer")
            {
                filter.Predicate = (x => x.Developer.Name == name);

            }
            //add filter to collection of filters
            filters.Add(filter);
            //reset cookie
            Session["GamesFilter"] = filters;

        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Developers = gameService.GetAllDevelopers();
            ViewBag.Genres = gameService.GetGenres();
            return View();

        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(GameViewModel item, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Bitmap convertedPicture = null;
                string filename = Guid.NewGuid().ToString() + ".jpg";
                string fullName = Server.MapPath("~/images/") + filename;
                if (image != null)
                {

                    convertedPicture = CustomImageConvertor.ConvertToBitmap(new System.Drawing.Bitmap(image.InputStream), 200, 300);
                    if (convertedPicture != null)
                    {
                        convertedPicture.Save(fullName, ImageFormat.Jpeg);
                        item.Image = "/images/" + filename;
                    }
                }
                else
                {

                    if (item.Image.Contains("data:image/png;base64"))
                    {
                        string str = item.Image.Replace("data:image/png;base64,", "");

                        System.Text.StringBuilder sbText = new System.Text.StringBuilder(str, str.Length);
                        sbText.Replace("\r\n", String.Empty); sbText.Replace(" ", String.Empty);

                        convertedPicture = CustomImageConvertor.ConvertToBitmap(new System.Drawing.Bitmap(new MemoryStream(Convert.FromBase64String(sbText.ToString()))), 200, 300);
                        if (convertedPicture != null)
                        {
                            convertedPicture.Save(fullName, ImageFormat.Jpeg);
                            item.Image = "/images/" + filename;
                        }
                    }
                    else
                    {
                        WebRequest request = WebRequest.Create(item.Image);
                        WebResponse response = request.GetResponse();
                        using (Stream stream = response.GetResponseStream())
                        {
                            convertedPicture = CustomImageConvertor.ConvertToBitmap(new System.Drawing.Bitmap(stream), 200, 300);
                        }
                        if (convertedPicture != null)
                        {
                            convertedPicture.Save(fullName, ImageFormat.Jpeg);
                            item.Image = "/images/" + filename;
                        }
                    }

                }
                gameService.AddGame(mapper.Map<Game>(item));
                return RedirectToAction("Index");
            }

            return Create();

        }
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Developers = gameService.GetAllDevelopers();
            ViewBag.Genres = gameService.GetGenres();
            var game = gameService.Find(id);
            return View(mapper.Map<GameViewModel>(game));
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(GameViewModel item)
        {
            gameService.Update(mapper.Map<Game>(item));
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult ShowByGenre(string genre, string developer)
        {
            ViewBag.Images = gameService.GetImages();
            ViewBag.Developers = gameService.GetAllDevelopers();
            ViewBag.Genres = gameService.GetGenres();
            if (developer == null)
            {
                var games = gameService.GetByGenre(genre);
                return View(mapper.Map<ICollection<GameViewModel>>(games));
            }
            else if (genre == null)
            {
                var games = gameService.GetByDeveloper(developer);
                return View(mapper.Map<ICollection<GameViewModel>>(games));
            }
            else
            {
                return View(mapper.Map<ICollection<GameViewModel>>(gameService.GetAllGames()));
            }
        }

        [HttpGet]
        public ActionResult ShowDetails(int id)
        {
            ViewBag.Developers = gameService.GetAllDevelopers();
            ViewBag.Genres = gameService.GetGenres();
            var game = gameService.Find(id);
            return View(mapper.Map<GameViewModel>(game));
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var game = gameService.Find(id);
            gameService.Delete(game);
            
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int id)
        {
            var units = Session["Cart"] as List<CartUnit>;
            bool isPresent = false;
            var item = mapper.Map<GameViewModel>(gameService.Find(id));
            foreach (var unit in units)
            {
                if (unit.Game.Name == item.Name)
                {
                    isPresent = true;
                    if (item.Amount > unit.Amount)
                        unit.Amount++;
                }
                    
            }
            if (!isPresent)
            units.Add(new CartUnit { Game = item, Amount = 1 });
            Session["Cart"] = units;
            return RedirectToAction("Index");
        }
    }
}