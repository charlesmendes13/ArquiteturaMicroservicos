
using AutoMapper;
using Identity.Application.ViewModels;
using Identity.Domain.Interfaces.Services;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccessTokenService _accessTokenService;

        public AccessTokenController(IMapper mapper,
            IAccessTokenService accessTokenService)
        {
            _mapper = mapper;
            _accessTokenService = accessTokenService;
        }

        [HttpPost]
        public async Task<ActionResult<AccessTokenViewModel>> Post(GetAccessTokenViewModel viewModel)
        {
            var token = await _accessTokenService.GetAcessTokenByUserAsync(_mapper.Map<User>(viewModel));

            if (token != null)
            {
                return Ok(_mapper.Map<AccessTokenViewModel>(token));
            }

            return Unauthorized();
        }
    }
}
