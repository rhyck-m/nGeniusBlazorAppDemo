using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nGeniusBlazorAppDemo.Server.Configuration
{
    public class NGeniusOptions
    {
        public string ApiKey { get; set; }
        public string HostedSessionID { get; set; }
        public string OutletRefID { get; set; }
        public string AccessTokenAPIURL { get; set; }
    }
}
