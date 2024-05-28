using System.Collections.ObjectModel;
using Newtonsoft.Json;
using RestSharp;
using TriviaGame.Models;

namespace TriviaGame.Services;

public static class TriviaQuestionsService
{
    public static async Task<TriviaModel> GetTriviaQuestions()
    {
        var options = new RestClientOptions("https://opentdb.com");
        var client = new RestClient(options);
        var request = new RestRequest("/api.php?amount=10&category=15&difficulty=easy&type=multiple", Method.Get);
        
        RestResponse response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            return new TriviaModel();

        return JsonConvert.DeserializeObject<TriviaModel>(response.Content) ?? new TriviaModel();
    }
}
