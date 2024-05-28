using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TriviaGame.Models;
using TriviaGame.Services;

namespace TriviaGame.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Result> triviaQuestions = [];

    [ObservableProperty]
    Result currentQuestion;

    [ObservableProperty]
    string progress = string.Empty;

    [ObservableProperty]
    bool isLoading;

    [ObservableProperty]
    bool showContent;

    int QuestionsIndex = 0;
    int CorrectAnswers = 0;
    int WrongAnswers = 0;

    [RelayCommand]
    async Task GetTriviaQuestions()
    {
        ShowLoading(true);

        var result = await TriviaQuestionsService.GetTriviaQuestions();
        TriviaQuestions = result.Results;
        
        ShowLoading(false);
    }

    void ShowLoading(bool state)
    {
        if (state){
            ShowContent = false;
            IsLoading = true;
            return;
        }

        IsLoading = false;
        ShowContent = true;
    }

    [RelayCommand]
    async Task NextQuestion()
    {
        QuestionsIndex++;
        UpdateProgess();

        if (QuestionsIndex > TriviaQuestions.Count)
        {
            await GameFinished();
            return;
        }

        CurrentQuestion = TriviaQuestions[QuestionsIndex - 1];
    }

    void UpdateProgess()
    {
        Progress = $"Question: {QuestionsIndex} of {TriviaQuestions.Count}";
    }

    async Task GameFinished()
    {
        await Shell.Current.DisplayAlert(
            "Trivia Gmae", 
            $"Game has Finished. Here are your details: Correct {CorrectAnswers}, Wrong {WrongAnswers}.", 
            "Ok"
        );
        await ResetGame();
    }

    async Task ResetGame()
    {
        TriviaQuestions = [];

        QuestionsIndex =
        CorrectAnswers = 
        WrongAnswers = 0;

        await GetTriviaQuestions();
        await NextQuestion();
    }

    [RelayCommand]
    async Task CheckAnswer(string answer)
    {
        if (string.Equals(answer, CurrentQuestion.Correct_answer))
        {
            await Shell.Current.DisplayAlert("Trivia Game", "Correct Answer!!", "YEY!");
            CorrectAnswers++;
        }
        else
        {
            await Shell.Current.DisplayAlert("Trivia Game", "Wrong Answer!", "Ahhh!");
            WrongAnswers++;
        }
        
        await NextQuestion();
    }
}
