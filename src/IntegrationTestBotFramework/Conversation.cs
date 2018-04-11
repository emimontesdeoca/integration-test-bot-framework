using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestBotFramework
{
    public class Conversation
    {
        [JsonProperty("bot")]
        public ChannelAccount bot { get; set; }
        [JsonProperty("isGroup")]
        public bool isGroup { get; set; }
        [JsonProperty("members")]
        public ChannelAccount[] members { get; set; }
        [JsonProperty("topicName")]
        public string topicName { get; set; }
        //[JsonProperty("activity")]
        //public Activity activity { get; set; }

        public Conversation(string botid, string botname, string userid, string username, string topicName)
        {
            this.bot = new ChannelAccount(botid, botname);
            this.isGroup = false;
            this.members = new ChannelAccount[1];
            this.members[0] = new ChannelAccount(userid, username);
            this.topicName = topicName;
            //this.activity = activity;
        }
    }
}
