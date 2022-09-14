using DutchTreat_th.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat_th.Data
{
   public interface IDutchRepository
    {
        IEnumerable<Product> GetAllsProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders(bool includeUtems);
        Order GetOrderById(string username,int id);
        void AddEntity(object entity);
        IEnumerable<Order> GetOrdersByUser(string username, bool includeItems);
    }
}
