using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    public partial class EditPlayerViewModel : ObservableObject
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
        private string playerFirstName;

        [ObservableProperty]
        private string playerLastName;

        [ObservableProperty]
        private string playerPhoto;

        [ObservableProperty]
        private int playerPosition;

        private int current_player_id;

        public EditPlayerViewModel(int player_id)
        {
            current_player_id = player_id;
            getPlayerInfo(player_id);
        }

        private void getPlayerInfo(int player_id)
        {
            try
            {

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql1 = "SELECT first_name FROM `players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sql1, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                PlayerFirstName = sqlRd2[0].ToString();
                sqlRd2.Close();

                String sql2 = "SELECT last_name FROM `players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sql2, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                PlayerLastName = sqlRd2[0].ToString();
                sqlRd2.Close();

                String sql3 = "SELECT photo FROM `players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sql3, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                PlayerPhoto = sqlRd2[0].ToString().Replace("\\", "\\\\");
                sqlRd2.Close();

                String sql4 = "SELECT position FROM `players` WHERE `player_id`=" + player_id + ";";

                sqlCmd = new MySqlCommand(sql4, sqlConn);
                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                PlayerPosition = Int32.Parse(sqlRd2[0].ToString());
                sqlRd2.Close();

                sqlConn.Close();

            }
            catch (Exception ex)
            {
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

            PlayerPhoto = result.FullPath.Replace("\\", "\\\\");
        }

        [RelayCommand]
        private void ConfirmPlayerClicked()
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

                String sql3 = "UPDATE `players` SET " +
                    "`first_name` = '" + PlayerFirstName + "', " +
                    "`last_name` = '" + PlayerLastName + "', " +
                    "`photo` = '" + PlayerPhoto + "', " +
                    "`position` = '" + PlayerPosition + "' WHERE `players`.`player_id` = " + current_player_id + ";";

                sqlCmd = new MySqlCommand(sql3, sqlConn2);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {
                }
                sqlRd2.Close();

                sqlConn2.Close();

                Application.Current.MainPage.DisplayAlert("", "Your player has been edited succesfully!", "OK");

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
