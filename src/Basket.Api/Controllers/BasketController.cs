using AutoMapper;
using Basket.Application.ViewModels;
using Basket.Domain.Interfaces.Services;
using Basket.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;

        public BasketController(IMapper mapper,
            IBasketService basketService)
        {
            _mapper = mapper;
            _basketService = basketService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<BasketViewModel>> Get(string userId)
        {
            var basket = await _basketService.GetByUserIdAsync(userId);

            return Ok(_mapper.Map<BasketViewModel>(basket));
        }

        [HttpPost("AddItem")]
        public async Task<ActionResult> Post([FromQuery] string userId, [FromBody] CreateItemViewModel viewModel)
        {
            await _basketService.AddItemToBasketAsync(userId, _mapper.Map<Item>(viewModel));

            return Ok();
        }

        [HttpDelete("RemoveItem")]
        public async Task<ActionResult> Delete([FromQuery] string userId, [FromQuery] int itemId)
        {
            await _basketService.RemoveItemFromBasketAsync(userId, itemId);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> Delete(string userId)
        {
            await _basketService.DeleteByUserIdAsync(userId);

            return Ok();
        }
    }
}
