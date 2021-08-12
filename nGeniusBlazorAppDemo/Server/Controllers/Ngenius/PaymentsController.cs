using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nGeniusBlazorAppDemo.Server.Services.NGenius;
using nGeniusBlazorAppDemo.Shared.Models.Ngenius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Server.Controllers.Ngenius
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("GetNgeniusAppSecret")]
        public AppSecret GetNgeniusAppSecret()
        {
            return _paymentService.GetAppSecretAsync();
        }

        [HttpPost("ProcessPayment/{sessionID}")]
        public ngoOrderResponse ProcessPayment(ngoOrderRequest payment, string sessionID)
        {
            return _paymentService.ProcessPayment(sessionID, payment);
        }
    }
}
