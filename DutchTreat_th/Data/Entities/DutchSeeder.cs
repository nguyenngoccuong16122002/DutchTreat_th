using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DutchTreat_th.Data.Entities
{
    public class DutchSeeder
    {
        public readonly DutchDbContext _dutchDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchDbContext dutchDbContext, IWebHostEnvironment webHostEnvironment, UserManager<StoreUser> userManager)
        {
            _dutchDbContext = dutchDbContext;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        public async Task SeedAync()
        {
            _dutchDbContext.Database.EnsureCreated();
            StoreUser user = await _userManager.FindByEmailAsync("Shawn@dutchtreat.com");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Shawn",
                    LastName = "Wildermuth",
                    Email = "shawn@dutchtreat.com",
                    UserName = "shawn@dutchtreat.com",

                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in Seeder");
                }
            }
            if (!_dutchDbContext.products.Any())
            {
                var file = Path.Combine(_webHostEnvironment.ContentRootPath, "Data\\art.json");
                var json = File.ReadAllText(file);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                _dutchDbContext.products.AddRange(products);
                var order = _dutchDbContext.orders.Where(O => O.Id == 1).FirstOrDefault();
                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                             Product=products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price
                        }
                    };
                }
                //_dutchDbContext.orders.Add(order);
               //await _dutchDbContext.SaveChangesAsync();
                 _dutchDbContext.SaveChanges();
            }


        }
    }
}
