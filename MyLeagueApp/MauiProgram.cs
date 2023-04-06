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

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
