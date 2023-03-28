using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyLeagueApp.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class LeaguesViewModel : ObservableObject
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
        ObservableCollection<League> leagues;

        //List<Team> teams;
        public LeaguesViewModel()
        {
            Leagues = new ObservableCollection<League>();
            loadLeagues();
        }

        private void loadLeagues()
        {

            try
            {
                string league_count = "0";
                string league_id;
                string league_name;
                string league_date;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT COUNT(*) FROM `leagues`; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                league_count = sqlRd3[0].ToString();
                sqlRd3.Close();
                sqlConn.Close();

                int cr = Int32.Parse(league_count);

                if (cr == 0)
                {
                    //label.IsVisible = true;
                }

                for (int i = 0; i < cr; i++)
                {

                    sqlConn.Open();

                    String sql2 = "SELECT league_id FROM `leagues` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    league_id = sqlRd[0].ToString();
                    sqlRd.Close();

                    String sqlName = "SELECT name FROM `leagues` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sqlName, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    league_name = sqlRd2[0].ToString();
                    sqlRd2.Close();

                    String df_sql = "SELECT created_date FROM `leagues` WHERE 1 LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(df_sql, sqlConn);
                    sqlRdDF = sqlCmd.ExecuteReader();

                    while (sqlRdDF.Read())
                    {

                    }

                    league_date = sqlRdDF[0].ToString().Substring(0, 10);

                    sqlRdDF.Close();

                    sqlConn.Close();

                    Leagues.Add(new League(Int32.Parse(league_id), league_name, league_date));

                }

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }

        }

        [RelayCommand]
        void NewLeague()
        {
            Shell.Current.GoToAsync(nameof(NewLeaguePage));
        }
    }
}
