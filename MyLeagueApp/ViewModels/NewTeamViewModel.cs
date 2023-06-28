using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Samples;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
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

        [ObservableProperty]
        ObservableCollection<TeamSample> teams;

        [ObservableProperty]
        TeamSample selected_team;

        public NewTeamViewModel()
        {
            Teams = new ObservableCollection<TeamSample>();
            loadSamples();
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

        private void loadSamples()
        {
            int team_id;
            string team_name;
            string team_city;
            string team_fullname;
            string team_abbreviation;
            string team_conference;
            string team_division;

            WebClient client = new WebClient();
            //String stats = client.DownloadString("https://free-nba.p.rapidapi.com/stats?seasons[]=2021&player_ids[]=237&rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688");
            String teams = client.DownloadString("https://free-nba.p.rapidapi.com/teams?rapidapi-key=ffe8de403amshdbfef1479d9fdafp10e8a0jsna7708bdc0688");
            dynamic data = JObject.Parse(teams);

            foreach (var member in data["data"])
            {
                team_id = (int)member["id"];
                team_city = (string)member["city"];
                team_name = (string)member["name"];
                team_fullname = (string)member["full_name"];
                team_abbreviation = (string)member["abbreviation"];
                team_conference = (string)member["conference"];
                team_division = (string)member["division"];

                Teams.Add(new TeamSample(0, team_id, team_name, team_city, team_abbreviation, team_division, team_conference, team_fullname));
            }

        }

        private void loadTeams()
        {
            try
            {

                int team_count;
                string team_id;
                string team_name;
                string team_city;
                string team_logo;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sqlc = "SELECT COUNT(*) FROM `teams`; ";

                sqlCmd = new MySqlCommand(sqlc, sqlConn);
                sqlRdC = sqlCmd.ExecuteReader();

                while (sqlRdC.Read())
                {

                }

                team_count = Int32.Parse(sqlRdC[0].ToString());
                sqlRdC.Close();

                sqlConn.Close();

                for (int i = 0; i < team_count; i++)
                {

                    sqlConn.Open();

                    String sql = "SELECT team_id FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    team_id = sqlRd[0].ToString();
                    sqlRd.Close();

                    String sql2 = "SELECT name FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql2, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    team_name = sqlRd2[0].ToString();
                    sqlRd2.Close();

                    String df_sql = "SELECT city FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(df_sql, sqlConn);
                    sqlRdDF = sqlCmd.ExecuteReader();

                    while (sqlRdDF.Read())
                    {

                    }

                    team_city = sqlRdDF[0].ToString();

                    sqlRdDF.Close();

                    String sqlLogo = "SELECT logo FROM `teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sqlLogo, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    team_logo = sqlRd3[0].ToString();

                    sqlRd3.Close();

                    sqlConn.Close();

                    //Teams.Add(new Team(Int32.Parse(team_id), team_name, team_city, team_logo));

                }

            }
            catch (Exception ex)
            {
                //DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }

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
        async void AddTeam()
        {
            if (Selected_team != null)
            {
                bool answer = await Shell.Current.DisplayAlert("Are you sure you want to sample this team?",
                                                          "City: " + Selected_team.City + "\n" +
                                                          "Name: " + Selected_team.Name + "\n" +
                                                          "Abbreviation: " + Selected_team.Abbreviation + "\n" +
                                                          "Conference: " + Selected_team.Conference + "\n" +
                                                          "Division: " + Selected_team.Division + "\n"
                                                          , "Yes", "No");

                if (answer)
                {
                    try
                    {

                        int team_id;

                        sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                    ";password=" + password +
                                                    ";database=" + database +
                                                    ";convert zero datetime=True";

                        sqlConn.Open();

                        String sql = "SELECT team_id+1 FROM `sampled_teams` ORDER BY team_id DESC LIMIT 0,1; ";

                        sqlCmd = new MySqlCommand(sql, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        team_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        team_name = name;
                        team_city = city;

                        String sql2 = "INSERT INTO `sampled_teams` (`city`, `name`, `abbreviation`, `conference`, `division`, `api_id`) " +
                            "VALUES ('" + Selected_team.City + "', '" + Selected_team.Name + "', '" + Selected_team.Abbreviation + "', '" + Selected_team.Conference + "', '" + Selected_team.Division + "', '" + Selected_team.ApiId + "');";

                        sqlCmd = new MySqlCommand(sql2, sqlConn);

                        sqlRd2 = sqlCmd.ExecuteReader();

                        while (sqlRd2.Read())
                        {

                        }

                        sqlRd2.Close();

                        sqlConn.Close();

                        Application.Current.MainPage.DisplayAlert("", "Your new sample team has been created!", "OK");

                        Shell.Current.GoToAsync(nameof(TeamsPage));

                        Selected_team = null;

                        Task.CompletedTask.Dispose();

                    }
                    catch (Exception ex)
                    {
                        Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                        sqlConn.Close();
                    }
                }

            }
            else
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

                        String sql = "SELECT team_id+1 FROM `teams` ORDER BY team_id DESC LIMIT 0,1; ";

                        sqlCmd = new MySqlCommand(sql, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        team_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        team_name = name;
                        team_city = city;

                        String sql2 = "INSERT INTO `teams` (`city`, `name`, `logo`) " +
                            "VALUES ('" + team_city + "', '" + team_name + "', '" + team_logo + "');";

                        sqlCmd = new MySqlCommand(sql2, sqlConn);

                        sqlRd2 = sqlCmd.ExecuteReader();

                        while (sqlRd2.Read())
                        {

                        }

                        sqlRd2.Close();

                        sqlConn.Close();

                        Application.Current.MainPage.DisplayAlert("", "Your new team has been created!", "OK");

                        Shell.Current.GoToAsync(nameof(TeamsPage));

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
}
