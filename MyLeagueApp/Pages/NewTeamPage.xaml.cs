namespace MyLeagueApp;
using Microsoft.Maui.Storage;
using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.IO;
using System.Data;

public partial class NewTeamPage : ContentPage
{
    public string team_name;
    public string team_city;
    public string team_logo;

    MySqlConnection sqlConn = new MySqlConnection();
    MySqlCommand sqlCmd = new MySqlCommand();
    MySqlDataReader sqlRd;
    MySqlDataReader sqlRd2;

    ViewCell lastCell;

    String server = "localhost";
    String username = "root";
    String password = "";
    String database = "myleague";

    public NewTeamPage()
	{
		InitializeComponent();
        BindingContext = new NewTeamViewModel();
    }

    private async void ChoosePhotoClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick Image",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null) return;

        team_logo = result.FullPath.Replace("\\", "\\\\");
    }

    private void AddTeamClicked(object sender, EventArgs e)
    {
        if (team_logo == null)
        {
            DisplayAlert("ATTENTION", "You haven't picked a logo for the team yet!", "OK");
        }
        else
        {
            try
            {

                int team_id;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT COUNT(*)+1 FROM `teams`; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                team_id = Int32.Parse(sqlRd[0].ToString());
                sqlRd.Close();

                team_name = name.Text;
                team_city = city.Text;

                String sql2 = "INSERT INTO `teams` (`team_id`, `city`, `name`, `logo`) " +
                    "VALUES (" + team_id + ", '" + team_city + "', '" + team_name + "', '" + team_logo + "');";

                sqlCmd = new MySqlCommand(sql2, sqlConn);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                sqlRd2.Close();

                sqlConn.Close();

                Navigation.PushAsync(new TeamsPage());

            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "OK");
            }
        }
    }

}