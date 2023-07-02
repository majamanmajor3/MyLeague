using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Stats;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{

    public partial class EditTeamViewModel : ObservableObject
    {

        MySqlConnection sqlConn = new MySqlConnection();
        MySqlConnection sqlConn2 = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        MySqlDataReader sqlRdC;
        MySqlDataReader sqlRd2;
        MySqlDataReader sqlRd3;
        MySqlDataReader sqlRd4;
        MySqlDataReader sqlRd5;
        MySqlDataReader sqlRd6;
        MySqlDataReader sqlRd7;
        MySqlDataReader sqlRd8;
        MySqlDataReader sqlRdDF;
        MySqlDataReader sqlRdA1;
        MySqlDataReader sqlRdA2;
        MySqlDataReader sqlRdA3;
        MySqlDataReader sqlRdA4;
        MySqlDataReader sqlRdA5;
        MySqlDataReader sqlRdA6;
        MySqlDataReader sqlRdDate;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        private string teamName;

        [ObservableProperty]
        private string teamCity;

        [ObservableProperty]
        private string teamLogo;

        [ObservableProperty]
        private string arenaName;

        [ObservableProperty]
        private string arenaCity;

        [ObservableProperty]
        private string arenaState;

        [ObservableProperty]
        private double arenaLatitude;

        [ObservableProperty]
        private double arenaLongitude;

        private int current_team_id;

        public EditTeamViewModel(int team_id)
        {
            current_team_id = team_id;
            getTeamInfo(team_id);
        }

        private void getTeamInfo(int team_id)
        {
            try
            {

                string team_name;
                string team_city;
                string team_logo;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql2 = "SELECT name FROM `teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sql2, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                TeamName = sqlRd2[0].ToString();
                sqlRd2.Close();

                String df_sql = "SELECT city FROM `teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(df_sql, sqlConn);
                sqlRdDF = sqlCmd.ExecuteReader();

                while (sqlRdDF.Read())
                {

                }

                TeamCity = sqlRdDF[0].ToString();

                sqlRdDF.Close();

                String sqlLogo = "SELECT logo FROM `teams` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlLogo, sqlConn);
                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                TeamLogo = sqlRd3[0].ToString().Replace("\\", "\\\\"); ;

                sqlRd3.Close();

                int arena_id;
                string arena_name;
                string arena_city;
                string arena_state;
                double arena_latitude;
                double arena_longitude;

                String sqlArena1 = "SELECT arena_id FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena1, sqlConn);
                sqlRdA1 = sqlCmd.ExecuteReader();

                while (sqlRdA1.Read())
                {

                }

                arena_id = Int32.Parse(sqlRdA1[0].ToString());

                sqlRdA1.Close();

                String sqlArena2 = "SELECT name FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena2, sqlConn);
                sqlRdA2 = sqlCmd.ExecuteReader();

                while (sqlRdA2.Read())
                {

                }

                ArenaName = sqlRdA2[0].ToString();

                sqlRdA2.Close();

                String sqlArena3 = "SELECT city FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena3, sqlConn);
                sqlRdA3 = sqlCmd.ExecuteReader();

                while (sqlRdA3.Read())
                {

                }

                ArenaCity = sqlRdA3[0].ToString();

                sqlRdA3.Close();

                String sqlArena4 = "SELECT state FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena4, sqlConn);
                sqlRdA4 = sqlCmd.ExecuteReader();

                while (sqlRdA4.Read())
                {

                }

                ArenaState = sqlRdA4[0].ToString();

                sqlRdA4.Close();

                String sqlArena5 = "SELECT latitude FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena5, sqlConn);
                sqlRdA5 = sqlCmd.ExecuteReader();

                while (sqlRdA5.Read())
                {

                }

                ArenaLatitude = Double.Parse(sqlRdA5[0].ToString());

                sqlRdA5.Close();

                String sqlArena6 = "SELECT longitude FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena6, sqlConn);
                sqlRdA6 = sqlCmd.ExecuteReader();

                while (sqlRdA6.Read())
                {

                }

                ArenaLongitude = Double.Parse(sqlRdA6[0].ToString());

                sqlRdA6.Close();

                sqlConn.Close();

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

            TeamLogo = result.FullPath.Replace("\\", "\\\\");
        }

        [RelayCommand]
        private void ConfirmTeamClicked()
        {
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

                String sql2 = "UPDATE `teams` SET " +
                    "`city` = '" + TeamCity + "', " + 
                    "`name` = '" + TeamName + "', " + 
                    "`logo` = '" + TeamLogo + "' WHERE `teams`.`team_id` = " + current_team_id + ";";

                sqlCmd = new MySqlCommand(sql2, sqlConn2);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {
                }
                sqlRd2.Close();

                String sql3 = "SELECT COUNT(*) FROM `arenas` WHERE `team_id`=" + current_team_id + ";";

                sqlCmd = new MySqlCommand(sql3, sqlConn2);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {
                }

                int check = Int32.Parse(sqlRd2[0].ToString());
                sqlRd2.Close();

                if (check == 0)
                {
                    sql3 = "INSERT INTO `arenas` (`name`, `city`, `state`, `latitude`, `longitude`, `team_id`) " +
                        "VALUES ('" + ArenaName + "', '" + ArenaCity + "', '" + ArenaState + "', '" + ArenaLatitude + "', '" + ArenaLongitude + "', '" + current_team_id + "');";

                    sqlCmd = new MySqlCommand(sql3, sqlConn2);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {
                    }
                    sqlRd2.Close();
                }
                else
                {
                    sql3 = "UPDATE `arenas` SET " +
                    "`name` = '" + ArenaName + "', " +
                    "`city` = '" + ArenaCity + "', " +
                    "`state` = '" + ArenaState + "', " +
                    "`latitude` = '" + ArenaLatitude + "', " +
                    "`longitude` = '" + ArenaLongitude + "' WHERE `arenas`.`team_id` = " + current_team_id + ";";

                    sqlCmd = new MySqlCommand(sql3, sqlConn2);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {
                    }
                    sqlRd2.Close();
                }

                sqlConn2.Close();

                Application.Current.MainPage.DisplayAlert("", "Your team has been edited succesfully!", "OK");

                Shell.Current.GoToAsync(nameof(TeamsPage));

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn2.Close();
            }
        }
    }
}
