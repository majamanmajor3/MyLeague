using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class SampledPlayerOverviewPage : ContentPage
{
    public int current_player_id;
    public SampledPlayerOverviewPage(int player_id)
	{
        InitializeComponent();
        current_player_id = player_id;
        BindingContext = new SampledPlayerOverviewPage(player_id);
    }
}