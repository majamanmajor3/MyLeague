using MyLeagueApp.ViewModels;

namespace MyLeagueApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(TeamsPage), typeof(TeamsPage));
		Routing.RegisterRoute(nameof(NewTeamPage), typeof(NewTeamPage));
		Routing.RegisterRoute(nameof(LeaguesPage), typeof(LeaguesPage));
		Routing.RegisterRoute(nameof(NewLeaguePage), typeof(NewLeaguePage));
		Routing.RegisterRoute(nameof(CurrentLeaguePage), typeof(CurrentLeaguePage));
		Routing.RegisterRoute(nameof(NewMatchViewModel), typeof(NewMatchViewModel));
		Routing.RegisterRoute(nameof(EditMatchViewModel), typeof(EditMatchViewModel));
	}
}
