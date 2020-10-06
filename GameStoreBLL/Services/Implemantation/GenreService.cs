using GameStoreBLL.Services.Abstraction;
using GameStoreDAL.Entities;
using GameStoreDAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreBLL.Services.Implemantation
{
    public class GenreService : IGenreService
    {
        private readonly IGenericRepository<Genre> repo;
        private readonly IGenericRepository<Game> repoGame;

        public GenreService(IGenericRepository<Genre> _repoGenre, IGenericRepository<Game> _repoGame)
        {
            repo = _repoGenre;
            repoGame = _repoGame;
        }
        public void Create(Genre genre)
        {
            repo.Create(genre);
        }

        public void Delete(Genre genre)
        {
            repo.Delete(genre);
        }

        public Genre Find(int id)
        {
            return repo.Find(id);
        }

        public ICollection<Genre> GetAll()
        {
            return repo.GetAll().ToList();
        }

        public bool IsUse(Genre genre)
        {
            if (repoGame.GetAll().Where(x => x.Genre.Name == genre.Name).Count() > 0)
                return true;
            else return false;
        }
    }
}
