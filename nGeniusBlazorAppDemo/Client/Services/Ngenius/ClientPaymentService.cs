using nGeniusBlazorAppDemo.Shared.Models.Ngenius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Client.Services.Ngenius
{
    public class ClientPaymentService : IClientPaymentService
    {
        private readonly HttpClient _httpClient;

        public ClientPaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AppSecret> GetAppSecrestAsync()
        {
            return await _httpClient.GetFromJsonAsync<AppSecret>("api/Payments/GetNgeniusAppSecret");
        }

        public async Task<ngoOrderResponse> MakePaymentAsync(string sessionID, ngoOrderRequest orderRequest)
        {
            var postResponse = await _httpClient.PostAsJsonAsync($"api/Payments/ProcessPayment/{sessionID}", orderRequest);
            if (postResponse.IsSuccessStatusCode)
            {
                return await postResponse.Content.ReadFromJsonAsync<ngoOrderResponse>();
            }
            return new ngoOrderResponse();
        }
    }
}
