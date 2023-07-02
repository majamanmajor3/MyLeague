namespace MyLeagueApp;
using Microsoft.Maui.Storage;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System.Data;

public partial class NewTeamPage : ContentPage
{
    public NewTeamPage()
	{
		InitializeComponent();
        BindingContext = new NewTeamViewModel();
    }

}