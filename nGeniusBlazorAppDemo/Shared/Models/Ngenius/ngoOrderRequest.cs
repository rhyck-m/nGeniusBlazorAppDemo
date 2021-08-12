using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Shared.Models.Ngenius
{
    public class ngoOrderRequest
    {
        public ngoOrderRequest()
        {
            amount = new ngoAmount();
            merchantAttributes = new ngoMerchantAttributes();
            billingAddress = new ngoBilling();
        }
        public string action { get; set; }
        public ngoAmount amount { get; set; }
        public string language { get; set; }
        public ngoMerchantAttributes merchantAttributes { get; set; }
        public ngoBilling billingAddress { get; set; }
        public string emailAddress { get; set; }
        public string merchantOrderReference { get; set; }
    }
}
