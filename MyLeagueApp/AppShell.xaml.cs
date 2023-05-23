using MyLeagueApp.Pages;
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
		Routing.RegisterRoute(nameof(SampledTeamsPage), typeof(SampledTeamsPage));
		Routing.RegisterRoute(nameof(SampledTeamOverviewPage), typeof(SampledTeamOverviewPage));
		Routing.RegisterRoute(nameof(SampledPlayerOverviewPage), typeof(SampledPlayerOverviewPage));
		Routing.RegisterRoute(nameof(NewSampledPlayerPage), typeof(NewSampledPlayerPage));
		Routing.RegisterRoute(nameof(NewMatchViewModel), typeof(NewMatchViewModel));
		Routing.RegisterRoute(nameof(EditMatchViewModel), typeof(EditMatchViewModel));
		Routing.RegisterRoute(nameof(TeamOverviewViewModel), typeof(TeamOverviewViewModel));
		Routing.RegisterRoute(nameof(NewPlayerViewModel), typeof(NewPlayerViewModel));
		Routing.RegisterRoute(nameof(MatchOverviewViewModel), typeof(MatchOverviewViewModel));
		Routing.RegisterRoute(nameof(LeagueLeadersViewModel), typeof(LeagueLeadersViewModel));
		Routing.RegisterRoute(nameof(EditTeamViewModel), typeof(EditTeamViewModel));
		Routing.RegisterRoute(nameof(EditPlayerViewModel), typeof(EditPlayerViewModel));
		Routing.RegisterRoute(nameof(SampledTeamsViewModel), typeof(SampledTeamsViewModel));
		Routing.RegisterRoute(nameof(SampledTeamOverviewViewModel), typeof(SampledTeamOverviewViewModel));
		Routing.RegisterRoute(nameof(SampledPlayerOverviewViewModel), typeof(SampledPlayerOverviewViewModel));
		Routing.RegisterRoute(nameof(NewSampledPlayerViewModel), typeof(NewSampledPlayerViewModel));
	}
}
