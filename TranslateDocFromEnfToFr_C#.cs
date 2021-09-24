// Using Translator Service - Azure Cognitve Services
//Youtube link: https://youtu.be/SROFZHdL07s

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocumentTranslator
{
    class Program
    {
        static readonly string route = "/batches";
        static readonly string endpoint = "<TRANSLATER_SERVICE_ENDPOINT>/translator/text/batch/v1.0";
        static readonly string key = "<TRANSLATER_SERVICE_KEY>";

        static readonly string json = ("" +
            "{\"inputs\": " +
                "[{\"source\": " +
                    "{\"sourceUrl\": \"<SOURCE_SAS_TOKEN>\"," +
                      "\"storageSource\": \"AzureBlob\"" +
                "}," +
            "\"targets\": " +
                "[{\"targetUrl\": \"<TARGET_SAS_TOKEN>\"," +
                   "\"storageSource\": \"AzureBlob\"," +
                    "\"language\": \"fr\"}]}]}");


        static async Task Main(string[] args)
        {
            using HttpClient client = new HttpClient();
            using HttpRequestMessage request = new HttpRequestMessage();
            {
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                request.Content = data;

                HttpResponseMessage response = await client.SendAsync(request);
                string result = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Operation successful with status code: {response.StatusCode}");
                }
                else
                    Console.Write($"Error occurred. Status code: {response.StatusCode}");
            }
        }
    }
}
