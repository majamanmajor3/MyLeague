using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class ComparisonPage : ContentPage
{
	public ComparisonPage()
	{
		InitializeComponent();
		BindingContext = new ComparisonViewModel();
	}
}