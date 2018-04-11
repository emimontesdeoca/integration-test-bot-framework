using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IntegrationTestBotFramework
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task TestFromFile()
        {
            // Load entries from file
            var path = System.IO.File.ReadAllText(@"C:\data.json");

            // Deserialize to object
            var data = JsonConvert.DeserializeObject<TestEntriesCollection>(path);

            foreach (var entry in data.Entries)
            {
                var token = "";

                if (entry.Request.Type == ActivityTypes.Message)
                {
                    using (var client = new WebClient())
                    {
                        #region TOKEN

                        /// Values for token
                        var values = new NameValueCollection();
                        values["grant_type"] = "client_credentials";
                        values["client_id"] = data.ClientId;
                        values["client_secret"] = data.ClientSecret;
                        values["scope"] = "https://graph.microsoft.com/.default";

                        /// POST
                        var response = client.UploadValues(data.AuthEndpoint, values);

                        /// Parse
                        var responseString = Encoding.Default.GetString(response);
                        var result = JsonConvert.DeserializeObject<ResponseObject>(responseString);

                        /// Token
                        token = result.access_token;

                        #endregion

                        #region CONVERSATION

                        /// Conversation data
                        var recipientId = "abcd1234";
                        var recipientName = "Bot";

                        var memberAddedfromId = "default-User";
                        var memberAddedfromName = "User";
                        var memberAddedbotId = "kik64dj2ik1l";
                        var memberAddedbotName = "Bot";

                        var conversationId = "123456789";

                        var channelId = "integration-test";

                        #region ADD USER

                        /// Conversation update -> add user
                        //var conversationUpdateAddUser = new Activity(ActivityTypes.ConversationUpdate)
                        //{
                        //    Text = "AddUser",
                        //    MembersAdded = new List<ChannelAccount>() { new ChannelAccount(memberAddedfromId, memberAddedfromName) },
                        //    Recipient = new ChannelAccount(recipientId, recipientName),
                        //    Conversation = new ConversationAccount(null, conversationId, null),
                        //    ChannelId = channelId,
                        //    ServiceUrl = "http://localhost:56739/" /// What do I have to put here? in the Bot emulator it displays this URL

                        //};

                        //client.Headers.Add("Content-Type", "application/json");
                        //client.Headers.Add("Authorization", $"Bearer {token}");
                        //client.UploadString(data.Endpoint, JsonConvert.SerializeObject(conversationUpdateAddUser));

                        ///// Clear header for token
                        //client.Headers.Clear();

                        #endregion

                        #region ADD BOT

                        ///// Conversation update -> add bot
                        //var conversationUpdateAddBot = new Activity(ActivityTypes.ConversationUpdate)
                        //{
                        //    Text = "AddBot",
                        //    MembersAdded = new List<ChannelAccount>() { new ChannelAccount(memberAddedbotId, memberAddedbotName) },
                        //    Recipient = new ChannelAccount(recipientId, recipientName),
                        //    Conversation = new ConversationAccount(null, conversationId, null),
                        //    ChannelId = channelId,
                        //    ServiceUrl = "http://localhost:56739/" /// What do I have to put here? in the Bot emulator it displays this URL

                        //};

                        //client.Headers.Add("Content-Type", "application/json");
                        //client.Headers.Add("Authorization", $"Bearer {token}");
                        //client.UploadString(data.Endpoint, JsonConvert.SerializeObject(conversationUpdateAddBot));

                        ///// Clear header for token
                        //client.Headers.Clear();

                        #endregion

                        #region START CONVERSATION

                        /// Conversation update->start conversation
                        var conversation = new Conversation(memberAddedfromId, memberAddedbotName, memberAddedfromId, memberAddedfromName, "test");

                        client.Headers.Add("Content-Type", "application/json");
                        client.Headers.Add("Authorization", $"Bearer {token}");

                        string json = JsonConvert.SerializeObject(conversation);
                        var responseBotMessage = client.UploadString(@"https://smba.trafficmanager.net/apis/v3/conversations", json);

                        #endregion

                        #region SEND MESSAGE

                        /// Conversation update -> send message
                        //var conversationUpdateSendMessage = new Activity(ActivityTypes.Message)
                        //{
                        //    Text = "Coche",
                        //    Locale = "es-ES",
                        //    From = new ChannelAccount(memberAddedfromId, memberAddedfromName),
                        //    Recipient = new ChannelAccount(recipientId, recipientName),
                        //    ReplyToId = memberAddedfromId,
                        //    Conversation = new ConversationAccount(null, conversationId, null),
                        //    ChannelId = channelId,
                        //    ServiceUrl = "http://localhost:56739/" /// What do I have to put here? in the Bot emulator it displays this URL

                        //};

                        //client.Headers.Add("Content-Type", "application/json");
                        //client.Headers.Add("Authorization", $"Bearer {token}");
                        //var responseBotMessage = client.UploadString(data.Endpoint, JsonConvert.SerializeObject(conversationUpdateSendMessage));

                        #endregion

                        #endregion

                    }
                }
            }

            await Task.CompletedTask;
        }


    }
}
