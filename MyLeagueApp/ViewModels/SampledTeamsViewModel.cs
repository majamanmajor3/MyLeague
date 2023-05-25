using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyLeagueApp.Classes.Samples;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class SampledTeamsViewModel : ObservableObject
    {
        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        MySqlDataReader sqlRdC;
        MySqlDataReader sqlRd2;
        MySqlDataReader sqlRd3;
        MySqlDataReader sqlRdDF;
        MySqlDataReader sqlRdDate;

        ViewCell lastCell;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        ObservableCollection<TeamSample> teams;

        //List<Team> teams;
        public SampledTeamsViewModel()
        {
            Teams = new ObservableCollection<TeamSample>();
            loadTeams();
        }

        private void loadTeams()
        {
            try
            {

                int team_count;
                string team_id;
                int team_api;
                string team_name;
                string team_city;
                string team_abbreviation;
                string team_conference;
                string team_division;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sqlc = "SELECT COUNT(*) FROM `sampled_teams`; ";

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

                    String sql = "SELECT team_id FROM `sampled_teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    team_id = sqlRd[0].ToString();
                    sqlRd.Close();

                    String sql_api = "SELECT api_id FROM `sampled_teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_api, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    team_api = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    String sql2 = "SELECT name FROM `sampled_teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql2, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    team_name = sqlRd2[0].ToString();
                    sqlRd2.Close();

                    String df_sql = "SELECT city FROM `sampled_teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(df_sql, sqlConn);
                    sqlRdDF = sqlCmd.ExecuteReader();

                    while (sqlRdDF.Read())
                    {

                    }

                    team_city = sqlRdDF[0].ToString();

                    sqlRdDF.Close();

                    String sqlAbbrv = "SELECT abbreviation FROM `sampled_teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sqlAbbrv, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    team_abbreviation = sqlRd3[0].ToString();

                    sqlRd3.Close();

                    String sqlDiv = "SELECT division FROM `sampled_teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sqlDiv, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    team_division = sqlRd3[0].ToString();

                    sqlRd3.Close();

                    String sqlConf = "SELECT conference FROM `sampled_teams` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sqlConf, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    team_conference = sqlRd3[0].ToString();

                    sqlRd3.Close();

                    sqlConn.Close();

                    Teams.Add(new TeamSample(Int32.Parse(team_id), team_api, team_name, team_city, team_abbreviation, team_division, team_conference, team_city + " " + team_name));

                }

            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("", ex.Message, "OK");
            }

        }

        //[RelayCommand]
        //async Task GoToDetails(TeamSample team)
        //{
        //    if (team == null)
        //        return;

        //    await Shell.Current.GoToAsync(nameof(TeamOverviewPage), true, new Dictionary<string, object>
        //{
        //   {"Team", team }
        //});

        //}

        [RelayCommand]
        void AddTeam()
        {
            Shell.Current.GoToAsync(nameof(NewTeamPage));
        }

        [RelayCommand]
        void CreatedTeams()
        {
            Shell.Current.GoToAsync(nameof(TeamsPage));
        }
    }
}
