using DutchTreat_th.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat_th.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchDbContext _dutchDbContext;
        private readonly ILogger<DutchRepository> _logger;
        public DutchRepository(DutchDbContext dutchDbContext,ILogger<DutchRepository> logger )
        {
            _dutchDbContext = dutchDbContext;
            _logger = logger;
        }

        public void AddEntity(object entity)
        {
            _dutchDbContext.Add(entity);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _dutchDbContext.orders
                  .Include(o => o.Items)
                  .ThenInclude(i => i.Product)
                  .ToList();
            }
            else
            {
                return _dutchDbContext.orders
                  .ToList();
            }
        }



        public IEnumerable<Product> GetAllsProducts()
        {
          try
            {
                _logger.LogInformation("GetAllProducts was called...");
                return _dutchDbContext.products.OrderBy(d => d.Artist).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return _dutchDbContext.orders
               .Include(o => o.Items)
               .ThenInclude(i => i.Product)
               .Where(o=>o.Id==id&& o.User.UserName==username)
               .FirstOrDefault();
        }

        public IEnumerable<Order> GetOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _dutchDbContext.orders
                  .Include(o => o.Items)
                  .ThenInclude(i => i.Product)
                  .Where(o => o.User.UserName == username)
                  .ToList();
            }
            else
            {
                return _dutchDbContext.orders
                  .Where(o => o.User.UserName == username)
                  .ToList();
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _dutchDbContext.products.Where(d => d.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return _dutchDbContext.SaveChanges()>0;
        }
    }
}
