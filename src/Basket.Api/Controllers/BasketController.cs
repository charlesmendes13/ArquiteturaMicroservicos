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
        public async Task<ActionResult<IEnumerable<ItemViewModel>>> Get(string userId)
        {
            var items = await _basketService.GetListItemByUserId(userId);

            return Ok(_mapper.Map<IEnumerable<ItemViewModel>>(items));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromQuery] string userId, [FromBody] CreateItemViewModel viewModel)
        {
            await _basketService.AddItemToBasket(userId, _mapper.Map<Item>(viewModel));

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string userId, [FromQuery] int itemId)
        {
            await _basketService.RemoveItemFromBasket(userId, itemId);

            return Ok();
        }
    }
}
