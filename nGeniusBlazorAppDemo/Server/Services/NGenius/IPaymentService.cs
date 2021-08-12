using nGeniusBlazorAppDemo.Shared.Models.Ngenius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Server.Services.NGenius
{
    public interface IPaymentService
    {
        AppSecret GetAppSecretAsync();
        ngoOrderResponse ProcessPayment(string sessionID, ngoOrderRequest orderRequest);
        //Task<ngoOrderResponse> ProcessPaymentAsync(string sessionID, ngoOrderRequest orderRequest);
    }
}
