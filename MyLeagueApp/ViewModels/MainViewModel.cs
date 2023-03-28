using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyLeagueApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            //Teams = new ObservableCollection<Team>();
        }

        [RelayCommand]
        public void OpenTeams()
        {
            Shell.Current.GoToAsync(nameof(TeamsPage));
        }

        [RelayCommand]
        void OpenLeagues()
        {
            Shell.Current.GoToAsync(nameof(LeaguesPage));
        }

    }
}
