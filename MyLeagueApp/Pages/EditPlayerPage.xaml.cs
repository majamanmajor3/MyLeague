using MyLeagueApp.ViewModels;

namespace MyLeagueApp.Pages;

public partial class EditPlayerPage : ContentPage
{
    public int current_player_id;
    public EditPlayerPage(int player_id)
    {
        InitializeComponent();
        current_player_id = player_id;
        BindingContext = new EditPlayerViewModel(player_id);
    }
}