using AutoMapper;
using DutchTreat_th.Data;
using DutchTreat_th.Data.Entities;
using DutchTreat_th.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat_th.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _dutchRepository;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository dutchRepository, ILogger<ProductsController> logger,
            IMapper mapper, UserManager<StoreUser> userManager)
        {

            _dutchRepository = dutchRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public ActionResult Get(bool includeitems = true)
        {
            try
            {
                var username = User.Identity.Name;
                var result = _dutchRepository.GetOrdersByUser(username, includeitems);
                //return Ok(_dutchRepository.GetAllOrders());
                // var result = _dutchRepository.GetAllOrders(includeitems);
                return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products:{ex}");
                return BadRequest("Failed to get products");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _dutchRepository.GetOrderById(User.Identity.Name, id);
                if (order != null) return Ok(_mapper.Map<OrderViewModel>(order));
                else return NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products:{ex}");
                return BadRequest("Failed to get products");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var Neworder = new Order()
                    //{
                    //    OrderDate = order.OrderDate,
                    //    OrderNumber = order.OrderNumber,
                    //    Id = order.OrderId

                    //};
                    var Neworder = _mapper.Map<Order>(order);

                    if (Neworder.OrderDate == DateTime.MinValue)
                    {
                        Neworder.OrderDate = DateTime.Now;
                    }
                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    Neworder.User = currentUser;
                    _dutchRepository.AddEntity(Neworder);
                    if (_dutchRepository.SaveAll())
                    {
                        //var vm = new OrderViewModel()
                        //{
                        //    OrderId = Neworder.Id,
                        //    OrderDate = Neworder.OrderDate,
                        //    OrderNumber = Neworder.OrderNumber

                        //};

                        // return Created($"/api/order/{vm.OrderId}", vm);
                        return Created($"/api/orders/{Neworder.Id}", _mapper.Map<OrderViewModel>(Neworder));
                    }
                    else return NotFound();

                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to savea new order:{ex}");
                return BadRequest("Failed to get products");
            }
        }
    }
}
