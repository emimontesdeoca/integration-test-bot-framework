using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestBotFramework
{
    public class ResponseObject
    {
        [JsonProperty("token_type")]
        public string token_type { get; set; }
        [JsonProperty("expires_in")]
        public int expires_in { get; set; }
        [JsonProperty("ext_expires_in")]
        public int ext_expires_in { get; set; }
        [JsonProperty("access_token")]
        public string access_token { get; set; }
    }
}
