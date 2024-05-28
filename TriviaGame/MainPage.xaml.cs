using TriviaGame.ViewModels;

namespace TriviaGame;

public partial class MainPage : ContentPage
{
	readonly MainViewModel _vm;

	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
		LoadTriviaQuestions();
	}

	private async void LoadTriviaQuestions()
	{
		await _vm.GetTriviaQuestionsCommand.ExecuteAsync(null);
		_vm.NextQuestionCommand.Execute(null);
	}
}

