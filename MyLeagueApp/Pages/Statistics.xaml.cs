using MyLeagueApp.Classes;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System.Data;

namespace MyLeagueApp;

public partial class Statistics : ContentPage
{
    public Statistics(string league_name)
    {
		InitializeComponent();
        BindingContext = new StatisticsViewModel(league_name);
    }
}