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
                        values["scope"] = data.ClientId + "/.default";

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
                        var conversationUpdateAddUser = new Activity(ActivityTypes.ConversationUpdate)
                        {
                            Text = "AddUser",
                            MembersAdded = new List<ChannelAccount>() { new ChannelAccount(memberAddedfromId, memberAddedfromName) },
                            Recipient = new ChannelAccount(recipientId, recipientName),
                            Conversation = new ConversationAccount(null, conversationId, null),
                            ChannelId = channelId,
                            ServiceUrl = "http://localhost:56739/" /// What do I have to put here? in the Bot emulator it displays this URL

                        };

                        client.Headers.Add("Content-Type", "application/json");
                        client.Headers.Add("Authorization", $"Bearer {token}");
                        client.UploadString(data.Endpoint, JsonConvert.SerializeObject(conversationUpdateAddUser));

                        /// Clear header for token
                        client.Headers.Clear();

                        #endregion

                        #region ADD BOT

                        /// Conversation update -> add bot
                        var conversationUpdateAddBot = new Activity(ActivityTypes.ConversationUpdate)
                        {
                            Text = "AddBot",
                            MembersAdded = new List<ChannelAccount>() { new ChannelAccount(memberAddedbotId, memberAddedbotName) },
                            Recipient = new ChannelAccount(recipientId, recipientName),
                            Conversation = new ConversationAccount(null, conversationId, null),
                            ChannelId = channelId,
                            ServiceUrl = "http://localhost:56739/" /// What do I have to put here? in the Bot emulator it displays this URL

                        };

                        client.Headers.Add("Content-Type", "application/json");
                        client.Headers.Add("Authorization", $"Bearer {token}");
                        client.UploadString(data.Endpoint, JsonConvert.SerializeObject(conversationUpdateAddBot));

                        /// Clear header for token
                        client.Headers.Clear();

                        #endregion

                        #region SEND MESSAGE

                        /// Conversation update -> send message
                        var conversationUpdateSendMessage = new Activity(ActivityTypes.Message)
                        {
                            Text = "Coche",
                            From = new ChannelAccount(memberAddedfromId, memberAddedfromName),
                            Recipient = new ChannelAccount(recipientId, recipientName),
                            Conversation = new ConversationAccount(null, conversationId, null),
                            ChannelId = channelId,
                            ServiceUrl = "http://localhost:56739/" /// What do I have to put here? in the Bot emulator it displays this URL

                        };

                        client.Headers.Add("Content-Type", "application/json");
                        client.Headers.Add("Authorization", $"Bearer {token}");
                        var responseBotMessage = client.UploadString(data.Endpoint, JsonConvert.SerializeObject(conversationUpdateSendMessage));

                        #endregion

                        #endregion

                    }
                }
            }

            await Task.CompletedTask;
        }

        [TestMethod]
        public async Task TestHelloWorld()
        {
            //Test("./helloworldTest.json", "http://localhost:3979/api/messages");

            // Bot info
            var appid = "e32ac4c0-bd36-4507-bde2-08f56c555970";
            var secret = "jsaifYFW29[_ppSZFE095]{";

            // Endpoints
            var endpoint = "https://d373235d.ngrok.io/api/messages";
            var authendpoint = "https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token";

            // Load entries from file
            var activityText = System.IO.File.ReadAllText(@"C:\jsonBot.json");

            // Deserialize to object
            var entries = JsonConvert.DeserializeObject<TestEntry>(activityText);

            using (var httpClient = new HttpClient())
            {

                //foreach (var entry in entries.Entries)
                //{
                if (entries.Request.Type == "message")
                {

                    var activity = new Activity(ActivityTypes.Message)
                    {
                        Text = "Test",
                        From = new ChannelAccount("id", "name"),
                        Recipient = new ChannelAccount("recipid", "recipname"),
                        Conversation = new ConversationAccount(false, "id", "name"),
                        ChannelId = "Test",
                        ServiceUrl = "http://localhost:50629/" /// What do I have to put here? in the Bot emulator it displays this URL

                    };

                    //// Authorization endpoint
                    //var auth = new HttpRequestMessage(HttpMethod.Post, authendpoint);
                    //// Use client id and secret to get token
                    //var content = "grant_type=client_credentials&client_id=" + appid + "&client_secret=" + secret + "&scope=https%3A%2F%2Fapi.botframework.com%2F.default";
                    //auth.Content = new StringContent(content, System.Text.UTF8Encoding.UTF8, "application/x-www-form-urlencoded");

                    var token = "";

                    /// POST to authentication
                    //using (HttpResponseMessage authresponse = await httpClient.SendAsync(auth))
                    //{
                    //    /// Get response and deserialize to object
                    //    var responseActivity = await authresponse.Content.ReadAsAsync<ResponseObject>();
                    //    token = responseActivity.access_token;
                    //}
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["grant_type"] = "client_credentials";
                        values["client_id"] = appid;
                        values["client_secret"] = secret;
                        values["scope"] = appid + "/.default";

                        var response = client.UploadValues(authendpoint, values);

                        var responseString = Encoding.Default.GetString(response);
                        var result = JsonConvert.DeserializeObject<ResponseObject>(responseString);
                        token = result.access_token;
                    }
                    // Request to bot
                    //var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

                    ////Conversation c = new Conversation("123456", "testbot", "123asd", "testconv", "helo");
                    //// Bearer with token
                    //request.Headers.Add("Authorization", "Bearer " + token);

                    //// Activity
                    //request.Content = new StringContent(JsonConvert.SerializeObject(message), System.Text.UTF8Encoding.UTF8, "application/json");

                    using (var client = new WebClient())
                    {
                        client.Headers.Add("Content-Type", "application/json");
                        client.Headers.Add("Authorization", $"Bearer {token}");
                        var btmResponse = client.UploadString(endpoint, JsonConvert.SerializeObject(activity));
                    }

                    // Post to bot
                    //using (HttpResponseMessage response = await httpClient.SendAsync(request))
                    //{
                    //    var responsereceiver = response.Content;
                    //    //var responseActivity = await response.Content.ReadAsAsync<Activity>();
                    //    // TODO Esto hacerlo dinámico, leyendo del nodo "assert"
                    //    //Assert.IsTrue(responseActivity.AsMessageActivity().Text == entries.Response.AsMessageActivity().Text);
                    //}
                }
            }
            //}


            await Task.CompletedTask;
        }
    }
}
