using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;

namespace TranslatorConsoleExample {
 
  class Translate {

    private static string subscriptionKey = Subscription.GetKey();

    private const string endpoint = "https://api.cognitive.microsofttranslator.com/";

    private static async Task<string> Translation(string language, string text) {
      string uri = endpoint + "/translate?api-version=3.0&to=" + language;

      System.Object[] body = new System.Object[] { new { Text = text } };
      var requestBody = JsonConvert.SerializeObject(body);

      using (var client = new HttpClient())
      using (var request = new HttpRequestMessage()) {
        request.Method = HttpMethod.Post;
        request.RequestUri = new Uri(uri);
        request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

        HttpResponseMessage response = await client.SendAsync(request);
        var responseBody = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);

        return result;
      }
    }

    public static async Task<string> TranslateToSpanish(string text) {
      string translation = await Translation("es", text);

      TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(translation);
      TranslationResult result  = deserializedOutput[0];

      return result.Translations[0].Text;     
    }



  }
}
