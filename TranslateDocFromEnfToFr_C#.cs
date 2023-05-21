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
        static readonly string endpoint = "https://demodoctranslaterinstance.cognitiveservices.azure.com//translator/text/batch/v1.0";
        static readonly string key = "75a9c35b083d465ba7489cc26f34bacf";

        static readonly string json = ("" +
            "{\"inputs\": " +
                "[{\"source\": " +
                    "{\"sourceUrl\": \"https://demodoctranslate.blob.core.windows.net/inputdocs?sp=r&st=2023-05-21T15:30:53Z&se=2023-05-21T23:30:53Z&spr=https&sv=2022-11-02&sr=c&sig=AOu29paomCSfNlPENMRiPW1b25RqnH5wqF5kEpx7gag%3D\"," +
                      "\"storageSource\": \"AzureBlob\"" +
                "}," +
            "\"targets\": " +
                "[{\"targetUrl\": \"https://demodoctranslate.blob.core.windows.net/outputdocs?sp=rcwl&st=2023-05-21T15:34:01Z&se=2023-05-21T23:34:01Z&spr=https&sv=2022-11-02&sr=c&sig=IL66cqm8hDN1WezvQTq9IIR8o7AxEMltdsOEiOf2WXk%3D\"," +
                   "\"storageSource\": \"AzureBlob\"," +
                    "\"language\": \"en\"}]}]}");


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
