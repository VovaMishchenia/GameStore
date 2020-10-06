using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreBLL.Services.Abstraction
{
    public interface  IOrderService
    {
        ICollection<Order> GetAllOrders();
        Order Find(int id);
        void Delete(Order order);
        void Update(Order order);
        void AddOrder(Order order);
    }
}
