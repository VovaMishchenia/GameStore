using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDAL.Repository.Abstract
{
    public interface IGenericRepository<TEntity>
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Detach(TEntity entity);
        void Attach(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity Find(int id);
    }
}
