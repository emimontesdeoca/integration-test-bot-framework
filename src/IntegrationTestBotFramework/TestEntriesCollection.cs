using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestBotFramework
{
    public class TestEntriesCollection
    {
        [JsonProperty("clientid")]
        public string ClientId { get; set; }
        [JsonProperty("clientsecret")]
        public string ClientSecret { get; set; }
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }
        [JsonProperty("authendpoint")]
        public string AuthEndpoint { get; set; }
        [JsonProperty("entries")]
        public List<TestEntry> Entries { get; set; }
    }

}
