using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Intent;

namespace IntegrateSpeechWithLUIS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read text from Mic
            string text = GetTextFromMicAsync().Result;

            // Read intent
            var response = GetIntents("PREDICTION_KEY", "PREDICTION_ENDPOINT", "APP_ID", text).Result;

            Console.WriteLine(response);
        }

        private static async Task<string> GetTextFromMicAsync()
        {
            var config = SpeechConfig.FromSubscription("SPEECH_SERVICE_KEY", "REGION");
            using (var recognizer = new IntentRecognizer(config))
            {
                Console.WriteLine("Speak now...");
                var result = await recognizer.RecognizeOnceAsync();
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    return result.Text;
                }
                else
                {
                    return "Speech could not be recognized.";
                }
            }  
        }

        private static async Task<string> GetIntents(string predictionKey, string predionEndpoint, string appId, string text)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            try
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", predictionKey);
                queryString["query"] = text;
                var preEndpointUri = String.Format("{0}luis/prediction/v3.0/apps/{1}/slots/production/predict?{2}", predionEndpoint, appId, queryString);
                var response = await client.GetAsync(preEndpointUri);
                var res = await response.Content.ReadAsStringAsync();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
