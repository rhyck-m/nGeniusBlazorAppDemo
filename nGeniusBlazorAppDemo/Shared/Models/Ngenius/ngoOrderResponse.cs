using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Shared.Models.Ngenius
{
    public struct ngoOrderResponse
    {
        public struct Self
        {
            public string href { get; set; }
        }

        public struct Cnp3ds
        {
            public string href { get; set; }
        }

        public struct Cury
        {
            public string name { get; set; }
            public string href { get; set; }
            public bool templated { get; set; }
        }

        public struct Links
        {
            public Self self { get; set; }
            //public Cnp3ds __invalid_name__cnp:3ds { get; set; }
            public List<Cury> curies { get; set; }
        }

        public struct PaymentMethod
        {
            public string expiry { get; set; }
            public string cardholderName { get; set; }
            public string name { get; set; }
            public string pan { get; set; }
            public string cvv { get; set; }
        }

        public struct Amount
        {
            public string currencyCode { get; set; }
            public int value { get; set; }
        }

        public struct __invalid_type__3ds
        {
            public string acsUrl { get; set; }
            public string acsPaReq { get; set; }
            public string acsMd { get; set; }
        }

        public struct RootObject
        {
            public string _id { get; set; }
            public Links _links { get; set; }
            public PaymentMethod paymentMethod { get; set; }
            public string state { get; set; }
            public Amount amount { get; set; }
            public DateTime updateDateTime { get; set; }
            public string outletId { get; set; }
            public string orderReference { get; set; }
            public __invalid_type__3ds __invalid_name__3ds { get; set; }
        }

        //Closes
        public string _id { get; set; }
        public Dictionary<string, ngoLink> _links { get; set; }
        public string action { get; set; }
        public ngoAmount amount { get; set; }
        public string language { get; set; }
        public ngoMerchantAttributes merchantAttributes { get; set; }
        public string reference { get; set; }
        public string outletId { get; set; }
        public string createDateTime { get; set; }
        public Dictionary<string, string[]> paymentMethods { get; set; }
        //public string formattedAmount { get; set; }
        //public string formattedOrderSummary { get; set; }
        public Dictionary<string, ngoPayment[]> _embedded { get; set; }

        // derived values

        public string OrderReference { get { return this.reference; } }
        public string PaymentPageLink
        {
            get
            {
                string link = null;
                if (this._links.ContainsKey("payment")) { link = this._links["payment"].href; }
                return link;
            }
        }
        public string State { get { return this._embedded["payment"][0].state; } }
    }

    public struct ngoLink
    {

        public string href { get; set; }

    }

    public struct ngoPayment
    {

        public string _id { get; set; }
        public string outletId { get; set; }
        public string orderReference { get; set; }
        public string state { get; set; }
        public ngoAmount amount { get; set; }
        public string updateDateTime { get; set; }

    }
}
