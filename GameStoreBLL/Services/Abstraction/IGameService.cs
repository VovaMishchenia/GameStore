using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreBLL.Services.Abstraction
{
    public interface IGameService
    {
        ICollection<Game> GetAllGames();
        ICollection<Game> GetAllGames(List<GameFilter> filters);
        ICollection<Genre> GetAllGenres();
        ICollection<Developer> GetAllDevelopers();
        ICollection<Game> GetGamesByGenre(string genre);
        ICollection<string> GetImages();
        void AddGame(Game model);
        IEnumerable<string> GetGenres();
        Game Find(int id);

        ICollection<Game> GetByGenre(string genre);
        ICollection<Game> GetByDeveloper(string developer);
        void Delete(Game game);
        void Update(Game game);
        ICollection<Game> OrderByLow(List<GameFilter> filters);
        ICollection<Game> OrderByHeigh(List<GameFilter> filters);
        ICollection<Game> FindByName(string name);

    }
}
