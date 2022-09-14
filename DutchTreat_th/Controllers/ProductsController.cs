using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat_th.Data;
using Microsoft.Extensions.Logging;
using DutchTreat_th.Data.Entities;

namespace DutchTreat_th.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IDutchRepository _dutchRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository dutchRepository,ILogger<ProductsController> logger)
        {
           
            _dutchRepository = dutchRepository;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_dutchRepository.GetAllsProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products:{ex}");
                return BadRequest("Failed to get products");
            }
        }
       
    }
}
