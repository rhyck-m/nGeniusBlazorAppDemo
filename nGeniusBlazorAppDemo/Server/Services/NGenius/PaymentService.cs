using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using nGeniusBlazorAppDemo.Server.Configuration;
using nGeniusBlazorAppDemo.Shared.Models.Ngenius;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Server.Services.NGenius
{
    public class PaymentService : IPaymentService
    {
        private readonly IOptions<NGeniusOptions> _ngeniusOptions;
        //private readonly HttpClient _httpClient;

        public PaymentService(
            IOptions<NGeniusOptions> ngeniusOptions
            //HttpClient httpClient
            )
        {
            _ngeniusOptions = ngeniusOptions;
            //_httpClient = httpClient;
        }

        public AppSecret GetAppSecretAsync()
        {
            AppSecret appSecret = new();
            appSecret.ApiKey = _ngeniusOptions.Value.ApiKey;
            appSecret.HostedSessionID = _ngeniusOptions.Value.HostedSessionID;
            appSecret.OutletRefID = _ngeniusOptions.Value.OutletRefID;

            return appSecret;
        }

        public ngoOrderResponse ProcessPayment(string sessionID, ngoOrderRequest orderRequest)
        {
            try
            {
                string paymentURL = $"https://api-gateway.sandbox.ngenius-payments.com/transactions/outlets/{_ngeniusOptions.Value.OutletRefID}/payment/hosted-session/{sessionID}";
                AccessTokenRequestResponse accessTokenRequestResponse = GetAccessToken();

                WebRequest request = (HttpWebRequest)WebRequest.Create(paymentURL);
                request.Method = "POST";
                request.ContentType = "application/vnd.ni-payment.v2+json";
                request.Headers["Authorization"] = "Bearer " + accessTokenRequestResponse.access_token;

                string payString = JsonConvert.SerializeObject(orderRequest);
                byte[] payData = Encoding.ASCII.GetBytes(payString);

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(payData, 0, payData.Length);
                }

                WebResponse response = (HttpWebResponse)request.GetResponse(); // it fails here
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                ngoOrderResponse orderResponse = JsonConvert.DeserializeObject<ngoOrderResponse>(responseString);

                return orderResponse;
            }
            catch (Exception ex) // throwing (422) status code error ** Unprocessable Entity
            {

                throw;
            }
        }


        private AccessTokenRequestResponse GetAccessToken()
        {
            var client = new RestClient(_ngeniusOptions.Value.AccessTokenAPIURL);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/vnd.ni-identity.v1+json");
            request.AddHeader("Content-Type", "application/vnd.ni-identity.v1+json");
            request.AddHeader("Authorization", $"Basic {_ngeniusOptions.Value.ApiKey}");

            IRestResponse response = client.Execute(request);

            AccessTokenRequestResponse accessTokenRequestResponse = JsonConvert.DeserializeObject<AccessTokenRequestResponse>(response.Content);

            return accessTokenRequestResponse;
        }

    }
}
