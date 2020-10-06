using GameStoreDAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreDAL.Repository.Implementation
{
    public class EFRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class

    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> set;
        public EFRepository(DbContext _context)
        {
            context = _context;
            set = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {

            set.Add(entity);
            Save();


        }
        private void Save()
        {
            context.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            set.Remove(entity);
            Save();
        }

        public TEntity Find(int id)
        {
            return set.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return set.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            Save();
        }
        public void Detach(TEntity entity)
        {
            Detach(entity);
            Save();
        }
        public void Attach(TEntity entity)
        {
            set.Attach(entity);
            Save();
        }
    }
}
