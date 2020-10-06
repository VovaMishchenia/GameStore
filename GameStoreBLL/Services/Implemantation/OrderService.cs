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
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> repo;
       

        public OrderService(IGenericRepository<Order> _repo)
        {
            repo = _repo;
           

        }

        public void AddOrder(Order order)
        {
            repo.Create(order);
        }

        public void Delete(Order order)
        {
            repo.Delete(order);
        }

        public Order Find(int id)
        {
            return repo.Find(id);
        }

        public ICollection<Order> GetAllOrders()
        {
            return repo.GetAll().ToList();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
