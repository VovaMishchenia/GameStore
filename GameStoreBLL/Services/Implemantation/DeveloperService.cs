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
    public class DeveloperService:IDeveloperService
    {
        private readonly IGenericRepository<Developer> repo;
        private readonly IGenericRepository<Game> repoGame;

        public DeveloperService(IGenericRepository<Developer> _repoDeveloper, IGenericRepository<Game> _repoGame)
        {
            repo = _repoDeveloper;
            repoGame = _repoGame;
        }
        public void Create(Developer dev)
        {
            repo.Create(dev);
        }

        public void Delete(Developer dev)
        {
            repo.Delete(dev);
        }

        public Developer Find(int id)
        {
            return repo.Find(id);
        }

        public ICollection<Developer> GetAll()
        {
            return repo.GetAll().ToList();
        }

        public bool IsUse(Developer dev)
        {
            if (repoGame.GetAll().Where(x => x.Developer.Name == dev.Name).Count() > 0)
                return true;
            else return false;
        }
    }
}
