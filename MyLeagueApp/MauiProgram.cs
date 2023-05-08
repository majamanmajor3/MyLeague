using Microsoft.Extensions.Logging;
using MyLeagueApp.ViewModels;

namespace MyLeagueApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("VinaSans-Regular.ttf", "VinaSans");
				fonts.AddFont("Montserrat-Black.ttf", "MontserratBlack");
				fonts.AddFont("Montserrat-Medium.ttf", "Montserrat");
				fonts.AddFont("Montserrat-ExtraBoldItalic.ttf", "MontserratEBI");
			});

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<TeamsViewModel>();
		builder.Services.AddTransient<NewTeamViewModel>();
		builder.Services.AddTransient<LeaguesViewModel>();
        builder.Services.AddTransient<NewLeagueViewModel>();
        builder.Services.AddTransient<CurrentLeagueViewModel>();
        builder.Services.AddTransient<NewMatchViewModel>();
        builder.Services.AddTransient<EditMatchViewModel>();
        builder.Services.AddTransient<TeamOverviewViewModel>();
        builder.Services.AddTransient<NewPlayerViewModel>();
        builder.Services.AddTransient<MatchOverviewViewModel>();
        builder.Services.AddTransient<LeagueLeadersViewModel>();
        builder.Services.AddTransient<EditTeamViewModel>();
        builder.Services.AddTransient<EditPlayerViewModel>();
        builder.Services.AddTransient<SampledTeamsViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
