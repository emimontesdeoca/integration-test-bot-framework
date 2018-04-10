using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestBotFramework
{
    public class TestEntry
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("request")]
        public Activity Request { get; set; }

        [JsonProperty("response")]
        public Activity Response { get; set; }

        [JsonProperty("assert")]
        public string Assert { get; set; }
    }
}
