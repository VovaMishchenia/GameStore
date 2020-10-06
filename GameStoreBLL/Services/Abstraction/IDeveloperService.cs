using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreBLL.Services.Abstraction
{
    public interface IDeveloperService
    {
        ICollection<Developer> GetAll();
        void Create(Developer dev);
        void Delete(Developer dev);
        bool IsUse(Developer dev);
        Developer Find(int id);
    }
}
