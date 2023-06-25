using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyLeagueApp.Pages;

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

        [RelayCommand]
        public void OpenComparison()
        {
            Shell.Current.GoToAsync(nameof(ComparisonPage));
        }

        [RelayCommand]
        private async void OpenHint()
        {

            bool answer = await Application.Current.MainPage.DisplayAlert("Welcome!", "Welcome to the MyLeague app! Here is how to get started:" + "\n" +
                                                          "By pressing one of the 3 buttons, you will be sent to a different page and be able to use all of the functionalities of this program!" + "\n" +
                                                          "LEAGUES - Here you will be able to create your own leagues using the created teams. Create and manage matches within the leagues and check players' and teams' statistics and leaderboards." + "\n" +
                                                          "COMPARE - Here you will have the option to choose 2 imported players or teams, pick a respective season for each and compare them by basic and advanced statistics, metrics and graphs." + "\n" +
                                                          "TEAMS - Here you will be able to create or import your teams and players. You can view all of them by cycling between the CREATED TEAMS and SAMPLED TEAMS, and by clicking on them you can view their basic information, seasonal statistics and players, for which by clicking on them, you can view or import their personal seasonal stats." + "\n" +
                                                          "Have fun using our basketball league creating and stats comparing application!" + "\n" +
                                                          "Would you like an introduction to the basic statistics you will find across the app?"
                                                          , "Yes", "No");

            if (answer)
            {
                try
                {

                    await Application.Current.MainPage.DisplayAlert("Basic Stats Hint", "Here is a breakdown of all the basic statistics:" + "\n" +
                                                          "PPG - A player's acquired points per game" + "\n" +
                                                          "RPG - A player's acquired rebounds per game" + "\n" +
                                                          "APG - A player's performed assists per game" + "\n" +
                                                          "SPG - A player's acquired steals per game" + "\n" +
                                                          "BPG - A player's performed blocks per game" + "\n" +
                                                          "FGMPG - A player's succesful field goals (total shots) per game" + "\n" +
                                                          "FAMPG - A player's attempted field goals (total shots) per game" + "\n" +
                                                          "3SMPG - A player's succesful three point shots per game" + "\n" +
                                                          "3SAPG - A player's attempted three point shots per game" + "\n" +
                                                          "Teams' PPG - A team's total acquired points per game" + "\n" +
                                                          "Teams' APPG - The team's opponents' total acquired points per game"
                                                          , "OK");

                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                }
            }
        }

    }

}
