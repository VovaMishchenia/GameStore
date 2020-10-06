using Binbin.Linq;
using GameStoreBLL.Services.Abstraction;
using GameStoreDAL.Entities;
using GameStoreDAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GameStoreBLL.Services.Implemantation
{
    public class GameService : IGameService
    {
        private readonly IGenericRepository<Game> repo;
        private readonly IGenericRepository<Developer> repoDev;
        private readonly IGenericRepository<Genre> repoGenre;

        public GameService(IGenericRepository<Game> _repo, IGenericRepository<Developer> _repoDev,
            IGenericRepository<Genre> _repoGenre)
        {
            repo = _repo;
            repoDev = _repoDev;
            repoGenre = _repoGenre;

        }
        public ICollection<Game> GetAllGames()
        {
            return repo.GetAll().ToList();
        }
        public ICollection<Genre> GetAllGenres()
        {
            return repo.GetAll().Select(x => x.Genre).Distinct().ToList();
        }
        public ICollection<Developer> GetAllDevelopers()
        {
            return repo.GetAll().Select(x => x.Developer).Distinct().ToList();
        }

        public ICollection<Game> GetGamesByGenre(string genre)
        {
            return repo.GetAll().Where(x => x.Genre.Name == genre).ToList();
        }

        public ICollection<string> GetImages()
        {
            return repo.GetAll().Select(x => x.Image).Distinct().ToList();
        }

        public void AddGame(Game model)
        {
            var dev = repoDev.GetAll().FirstOrDefault(x => x.Name == model.Developer.Name);
            if (dev != null)
                model.Developer = dev;

            var genre = repoGenre.GetAll().FirstOrDefault(x => x.Name == model.Genre.Name);
            if (dev != null)
                model.Genre = genre;
            repo.Create(model);

        }

        public IEnumerable<string> GetGenres()
        {
            return repoGenre.GetAll().Select(x => x.Name);
        }

        public Game Find(int id)
        {
            return repo.Find(id);
        }

        public ICollection<Game> GetByGenre(string genre)
        {
            return repo.GetAll().Where(x => x.Genre.Name == genre).ToList();
        }

        public ICollection<Game> GetByDeveloper(string developer)
        {
            return repo.GetAll().Where(x => x.Developer.Name == developer).ToList();

        }
        /// <summary>
        /// Method will delete game from DB
        /// </summary>
        /// <param name="game">object to delete from table games</param>
        public void Delete(Game game)
        {
            repo.Delete(game);
        }

        public void Update(Game model)
        {
            var dev = repoDev.GetAll().FirstOrDefault(x => x.Name == model.Developer.Name);
            if (dev != null)
                model.Developer = dev;

            var genre = repoGenre.GetAll().FirstOrDefault(x => x.Name == model.Genre.Name);
            if (dev != null)
                model.Genre = genre;
            repo.Detach(model);
            repo.Update(model);
        }

        public ICollection<Game> GetAllGames(List<GameFilter> filters)
        {
            if (filters != null && filters.Count() != 0)
            {
                var collection = new List<Expression<Func<Game, bool>>>();
            foreach (var g in filters.GroupBy(x => x.Type))
                {
                    var predicate = PredicateBuilder.Create(g.ToArray()[0].Predicate);
                    for (int i = 1; i < g.Count(); i++)
                    {
                        predicate = predicate.Or(g.ToArray()[i].Predicate);
                    }
                    collection.Add(predicate);
                }

                var res = PredicateBuilder.Create(collection[0]);
                for (int i = 1; i < collection.Count; i++)
                {
                    res = res.And(collection[i]);
                }
                return repo.GetAll().Where(res.Compile()).ToList();
            }

            //if (filters != null && filters.Count() != 0)
            //{
            //   var group = filters.GroupBy(x => x.Type);
            //    var predicate = PredicateBuilder.Create(filters[0].Predicate);

            //    for (int i = 1; i < filters.Count; i++)
            //    {
            //        predicate = predicate.Or(filters[i].Predicate);
            //    }

            //    return repo.GetAll().Where(predicate.Compile()).ToList();
            //}
            return repo.GetAll().ToList();
        }

        public ICollection<Game> OrderByLow(List<Game> games)
        {
            return games.OrderBy(x => x.Price).ToList();
        }

        public ICollection<Game> OrderByLow(List<GameFilter> filters)
        {

            return GetAllGames(filters).OrderBy(x => x.Price).ToList();

        }

        public ICollection<Game> OrderByHeigh(List<GameFilter> filters)
        {
            return GetAllGames(filters).OrderBy(x => -x.Price).ToList();
        }

        public ICollection<Game> FindByName(string name)
        {
            var collection = new List<Game>();
            string str = @"^(.*?)(\b" + name + @"\b)(.*)$";
            Regex reg = new Regex(str);
            foreach (var item in repo.GetAll())
            {
                if (reg.IsMatch(item.Name))
                    collection.Add(item);
            }
            //^(.*?)(\bpass\b)(.*)$
            return collection;
        }
    }
}
