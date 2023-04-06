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
    partial class EditMatchViewModel : ObservableObject
    {
        MySqlConnection sqlConn2 = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        MySqlDataReader sqlRdC;
        MySqlDataReader sqlRd2;
        MySqlDataReader sqlRd3;
        MySqlDataReader sqlRd4;
        MySqlDataReader sqlRd7;
        MySqlDataReader sqlRd8;
        MySqlDataReader sqlRdDF;
        MySqlDataReader sqlRdDate;

        String current_league_name;
        int current_match_id;
        string date;
        int hometeam;
        int awayteam;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        ObservableCollection<Team> teams;

        private int homeScore;
        private int awayScore;
        private string homename;
        private string awayname;

        public int HomeScore
        {
            get { return homeScore; }
            set { homeScore = value; }
        }

        public int AwayScore
        {
            get { return awayScore; }
            set { awayScore = value; }
        }

        public string Homename
        {
            get { return homename; }
            set { homename = value; }
        }

        public string Awayname
        {
            get { return awayname; }
            set { awayname = value; }
        }


        public EditMatchViewModel(string league_name, int match_id)
        {
            Teams = new ObservableCollection<Team>();
            current_match_id = match_id;
            current_league_name = league_name;
            loadSelections();
        }

        private void loadSelections()
        {
            try
            {

                int home_team;
                int away_team;
                int home_score;
                int away_score;

                sqlConn2.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn2.Open();

                String sql1 = "SELECT home_team FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql1, sqlConn2);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                home_team = Int32.Parse(sqlRd[0].ToString());
                sqlRd.Close();

                String sql2 = "SELECT away_team FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql2, sqlConn2);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                away_team = Int32.Parse(sqlRd2[0].ToString());
                sqlRd2.Close();

                String sql3 = "SELECT home_score FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql3, sqlConn2);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                home_score = Int32.Parse(sqlRd3[0].ToString());
                sqlRd3.Close();

                String sql4 = "SELECT away_score FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql4, sqlConn2);
                sqlRd4 = sqlCmd.ExecuteReader();

                while (sqlRd4.Read())
                {

                }

                away_score = Int32.Parse(sqlRd4[0].ToString());
                sqlRd4.Close();

                String sql5 = "SELECT date FROM `" + current_league_name + "` WHERE `m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql5, sqlConn2);
                sqlRdDate = sqlCmd.ExecuteReader();

                while (sqlRdDate.Read())
                {

                }

                date = sqlRdDate[0].ToString();
                sqlRdDate.Close();

                String sql_h_name = "SELECT t.name FROM `" + current_league_name + "` l JOIN teams t ON (l.home_team = t.team_id) WHERE l.`m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql_h_name, sqlConn2);
                sqlRd7 = sqlCmd.ExecuteReader();

                while (sqlRd7.Read())
                {

                }

                string home = sqlRd7[0].ToString();
                homename = "Set " + home + " final score"; ;
                sqlRd7.Close();

                String sql_a_name = "SELECT t.name FROM `" + current_league_name + "` l JOIN teams t ON (l.away_team = t.team_id) WHERE l.`m_id` = " + current_match_id + "; ";

                sqlCmd = new MySqlCommand(sql_a_name, sqlConn2);
                sqlRd8 = sqlCmd.ExecuteReader();

                while (sqlRd8.Read())
                {

                }

                string away = sqlRd8[0].ToString();
                awayname = "Set " + away + " final score";
                sqlRd8.Close();

                sqlConn2.Close();

                //home_picker.SelectedItem = teams[home_team];
                //away_picker.SelectedItem = teams[away_team];
                hometeam = home_team;
                awayteam = away_team;
                homeScore = home_score;
                awayScore = away_score;


            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn2.Close();
            }

        }

        [RelayCommand]
        private void ConfirmMatchClicked()
        {
            //if (
            //    //home_picker == null || away_picker == null || 
            //    homeScore == null || awayScore == null)
            //{
            //    DisplayAlert("ATTENTION", "You haven't picked all data for the match yet!", "OK");
            //}
            //else
            //{
            try
            {

                int match_id;
                int home_team;
                int away_team;

                sqlConn2.Close();

                sqlConn2.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn2.Open();

                String sql = "SELECT COUNT(*)+1 FROM `" + current_league_name + "`; ";

                sqlCmd = new MySqlCommand(sql, sqlConn2);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                match_id = Int32.Parse(sqlRd[0].ToString());
                sqlRd.Close();

                //int home_index = home_picker.SelectedIndex;
                //home_team = teams[home_index].Id;

                //int away_index = away_picker.SelectedIndex;
                //away_team = teams[away_index].Id;

                home_team = hometeam;
                away_team = awayteam;

                String sql2 = "UPDATE `" + current_league_name + "` SET `home_score` = '" + homeScore + "', `away_score` = '" + awayScore + "' WHERE `" + current_league_name + "`.`m_id` = " + current_match_id + ";";

                sqlCmd = new MySqlCommand(sql2, sqlConn2);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                sqlRd2.Close();

                sqlConn2.Close();

                Application.Current.MainPage.DisplayAlert("", "Your match has been edited succesfully!", "OK");

                Shell.Current.GoToAsync(nameof(LeaguesPage));
                // oldd meg szebben !!!

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn2.Close();
            }
        }
    }
}
