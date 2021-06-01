using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepository repository, ILogger<BasketController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket([FromRoute] string userName)
        {
            var shoppingCart = await _repository.GetBasketAsync(userName);
            return shoppingCart ?? new ShoppingCart(userName);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            var shoppingCart = await _repository.UpdateBasketAsync(basket);
            return shoppingCart;
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket([FromRoute] string userName)
        {
            await _repository.DeleteBasketAsync(userName);
            return NoContent();
        }
    }
}
