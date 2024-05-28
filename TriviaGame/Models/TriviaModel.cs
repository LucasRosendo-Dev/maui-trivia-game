using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using TriviaGame.Helpers.Extensions;

namespace TriviaGame.Models;

public class TriviaModel
{
    public int Response_code { get; set; }
    public ObservableCollection<Result> Results { get; set; }
}

public class Result
{
    public string Type { get; set; }
    public string Difficulty { get; set; }
    public string Category { get; set; }
    public string Question { get; set; }
    public string Correct_answer { get; set; }
    public List<string> Incorrect_answers { get; set; }
    
    [JsonIgnore]
    public string DecodedQuestion => Question.DecodeHtml();

    [JsonIgnore]
    public List<string> AnswersList {
        get {
            var newAnswersList = new List<string>();
            newAnswersList.AddRange(Incorrect_answers);
            newAnswersList.Add(Correct_answer);

            var rng = new Random();
            var shuffledList = newAnswersList.OrderBy(_ => rng.Next()).ToList();

            var decodedList = new List<string>();

            foreach (var item in shuffledList)
            {
                decodedList.Add(item.DecodeHtml());
            }

            return decodedList;
        }
    }
}

