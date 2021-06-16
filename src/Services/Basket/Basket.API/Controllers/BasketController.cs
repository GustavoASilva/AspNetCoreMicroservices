using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository repository, ILogger<BasketController> logger,
                                IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var actualBasket = await _repository.GetBasketAsync(basketCheckout.UserName);
            if(actualBasket == null)
            {
                return BadRequest();
            }

            var basketCheckoutEvent = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            basketCheckoutEvent.TotalPrice = actualBasket.TotalPrice;

            await _publishEndpoint.Publish(basketCheckoutEvent);

            //await _repository.DeleteBasketAsync(basketCheckout.UserName);

            return Ok();
        }
    }
}
