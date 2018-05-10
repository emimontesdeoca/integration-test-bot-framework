using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace IntegrationTestBotFramework
{
    public class Utils
    {
        public static T uploadString<T>(string bearer, string url, string serializedJson)
        {
            string serializedResult = "";
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", $"Bearer {bearer}");

                serializedResult = client.UploadString(url, serializedJson);
            }

            return JsonConvert.DeserializeObject<T>(serializedResult);

        }

        public static T downloadString<T>(string bearer, string url)
        {
            string serializedResult = "";
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", $"Bearer {bearer}");

                serializedResult = client.DownloadString(url);
            }

            return JsonConvert.DeserializeObject<T>(serializedResult);

        }
    }
}
