using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyLeagueApp.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class TeamsViewModel : ObservableObject
    {
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
        ObservableCollection<Team> teams;

        //List<Team> teams;
        public TeamsViewModel()
        {
            Teams = new ObservableCollection<Team>();
            loadTeams();
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

                    Teams.Add(new Team(Int32.Parse(team_id), team_name, team_city, team_logo));

                }

            }
            catch (Exception ex)
            {
                //DisplayAlert("", ex.Message, "OK");
            }

        }

        [RelayCommand]
        void AddTeam()
        {
            Shell.Current.GoToAsync(nameof(NewTeamPage));
        }
    }
}
