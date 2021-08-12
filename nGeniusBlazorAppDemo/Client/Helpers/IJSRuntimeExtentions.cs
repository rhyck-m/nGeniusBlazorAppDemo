using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Client.Helpers
{
    public static class IJSRuntimeExtentions
    {
        // use this method to mount the card in the dom
        public static ValueTask InitNGeniusCard(this IJSRuntime js, string _ngeniusSessionID, string outletRef)
            => js.InvokeVoidAsync("AppNGenius.mountPaymentCard", _ngeniusSessionID, outletRef);

        
        // extent this method if you wish to use Javascript to process payment using 3Ds
        public static ValueTask<string> ProcessNGeniusPayment(this IJSRuntime js, string sessionID, string accessToken, string outletRef)
            => js.InvokeAsync<string>("AppNGenius.processPayment", sessionID, accessToken, outletRef);

        public static ValueTask<bool> ShowSwalAlert(this IJSRuntime js, string message, AlertMessageType alertMessageType)
        {
            return js.InvokeAsync<bool>("AppAlerts.ShowSwalAlert", message, alertMessageType.ToString());
        }
    }

    public enum AlertMessageType
    {
        question, warning, error, info, success
    }
}
