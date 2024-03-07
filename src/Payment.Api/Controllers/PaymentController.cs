using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.Application.ViewModels;
using Payment.Domain.Interfaces.Services;

namespace Payment.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public PaymentController(IMapper mapper,
            IPaymentService paymentService)
        {
            _mapper = mapper;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreatePaymentViewModel viewModel)
        {
            await _paymentService.InsertAsync(_mapper.Map<Domain.Models.Payment>(viewModel));

            return Ok();
        }
    }
}
