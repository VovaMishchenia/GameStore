using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreBLL.Services.Abstraction
{
    public interface IGenreService
    {
        ICollection<Genre> GetAll();
        void Create(Genre genre);
        void Delete(Genre genre);
        bool IsUse(Genre genre);
        Genre Find(int id);
    }
}
