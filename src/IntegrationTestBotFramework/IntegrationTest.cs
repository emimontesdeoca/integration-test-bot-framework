using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IntegrationTestBotFramework
{
    [TestClass]
    public class IntegrationTest
    {
        public class TestEntriesCollection
        {
            public List<TestEntry> Entries { get; set; }
        }

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


        [TestMethod]
        public async Task TestHelloWorld()
        {
            //Test("./helloworldTest.json", "http://localhost:3979/api/messages");

            var endpoint = "http://localhost:3979/api/messages";
            // TODO Cargar el fichero de texto
            var activityText = System.IO.File.ReadAllText(@"C:\jsonBot.json");
            var entries = JsonConvert.DeserializeObject<TestEntry>(activityText);

            using (var httpClient = new HttpClient())
            {

                //foreach (var entry in entries.Entries)
                //{
                if (entries.Request.Type == "message")
                { 
                    var activity = new Activity(ActivityTypes.Message)
                    {
                        Text = entries.Request.Text,
                        From = new ChannelAccount("id", "name"),
                        Recipient = new ChannelAccount("recipid", "recipname"),
                        Conversation = new ConversationAccount(false, "id", "name"),
                        ChannelId = "Test",
                        ServiceUrl = "http://localhost:50629/api/messages",
                        Attachments = entries.Request.Attachments,
                        Entities = entries.Request.Entities

                    };

                    var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                    request.Content = new StringContent(JsonConvert.SerializeObject(activity), System.Text.UTF8Encoding.Default, "application/json");

                    using (HttpResponseMessage response = await httpClient.SendAsync(request))
                    {
                        var responsereceiver = response.Content;
                        var responseActivity = await response.Content.ReadAsAsync<Activity>();
                        // TODO Esto hacerlo dinámico, leyendo del nodo "assert"
                        Assert.IsTrue(responseActivity.AsMessageActivity().Text == entries.Response.AsMessageActivity().Text);
                    }
                }
            }
            //}


            await Task.CompletedTask;
        }
    }
}
