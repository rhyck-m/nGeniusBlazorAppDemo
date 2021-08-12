using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using nGeniusBlazorAppDemo.Client.Helpers;
using nGeniusBlazorAppDemo.Client.Services.Ngenius;
using nGeniusBlazorAppDemo.Shared.Models.Ngenius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Client.Pages.Ngenius
{
    public class PlaceOrderBase : ComponentBase
    {
        protected AppSecret ngeniusAppSecret { get; set; }
        public ngoCustomer customer { get; set; } = new();

        public bool IsMountingCard { get; set; } = true;

        [Inject]
        public IClientPaymentService _clientPaymentService { get; set; }

        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ngeniusAppSecret = await _clientPaymentService.GetAppSecrestAsync();
                await jSRuntime.InitNGeniusCard(ngeniusAppSecret.HostedSessionID, ngeniusAppSecret.OutletRefID);

                IsMountingCard = false;
                StateHasChanged();
            }
            
        }

        protected async Task MakePayment()
        {
            string sessionID = await jSRuntime.InvokeAsync<string>("AppNGenius.getOrderSessionID");
            if (sessionID.IndexOf("Error") > -1)
            {
                await jSRuntime.ShowSwalAlert("Please provide valid credit card information", AlertMessageType.error);
                return;
            }

            Console.WriteLine(sessionID);

            // Make payment on the Server

            customer = new();
            customer.firstName = "Jon";
            customer.lastName = "Snow";
            customer.email = "jsnow@test.com";
            ngoOrderRequest ngoOrderRequest = new()
            {
                action = "SALE",
                amount = new ngoAmount() { currencyCode = "AED", value = 1500 },
                billingAddress = new ngoBilling() { firstName = customer.firstName, lastName = customer.lastName },
                emailAddress = customer.email
            };

            ngoOrderResponse orderResponse = await _clientPaymentService.MakePaymentAsync(sessionID, ngoOrderRequest);

            Console.WriteLine($"The payment response state :: {orderResponse.State}");

            // If the payment response is successfull
            // do some stuff 

            // If payment failed
            // alert the user for payment failure 

        }
    }
}
