using nGeniusBlazorAppDemo.Shared.Models.Ngenius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Client.Services.Ngenius
{
    public interface IClientPaymentService
    {
        Task<AppSecret> GetAppSecrestAsync();

        Task<ngoOrderResponse> MakePaymentAsync(string sessionID, ngoOrderRequest orderRequest);
    }
}
