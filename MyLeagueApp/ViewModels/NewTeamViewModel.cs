using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class NewTeamViewModel : ObservableObject
    {
        public string team_name;
        public string team_city;
        public string team_logo;

        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        DataTable sqlDt = new DataTable();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        MySqlDataReader sqlRdC;
        MySqlDataReader sqlRd2;
        MySqlDataReader sqlRd3;
        MySqlDataReader sqlRdDF;
        MySqlDataReader sqlRdDate;
        DataSet ds = new DataSet();

        ViewCell lastCell;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        public NewTeamViewModel()
        {
            //Teams = new ObservableCollection<Team>();
        }

        private string name;
        private string city;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        [RelayCommand]
        private async void ChoosePhotoClicked()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Image",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null) return;

            team_logo = result.FullPath.Replace("\\", "\\\\");
        }

        [RelayCommand]
        private void AddTeam()
        {
            if (team_logo == null)
            {
                Application.Current.MainPage.DisplayAlert("ATTENTION", "You haven't picked a logo for the team yet!", "OK");
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

                    team_name = name;
                    team_city = city;

                    String sql2 = "INSERT INTO `teams` (`team_id`, `city`, `name`, `logo`) " +
                        "VALUES (" + team_id + ", '" + team_city + "', '" + team_name + "', '" + team_logo + "');";

                    sqlCmd = new MySqlCommand(sql2, sqlConn);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    sqlRd2.Close();

                    sqlConn.Close();

                    Application.Current.MainPage.DisplayAlert("", "Your new team has been created!", "OK");

                    Shell.Current.GoToAsync(nameof(MainPage));

                }
                catch (Exception ex)
                {
                    //DisplayAlert("", ex.Message, "OK");
                    sqlConn.Close();
                }
            }
        }
    }
}
