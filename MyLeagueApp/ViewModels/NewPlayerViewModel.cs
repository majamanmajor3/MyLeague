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
    partial class NewPlayerViewModel : ObservableObject
    {
        public string player_fname;
        public string player_lname;
        public string player_photo;
        public int player_position;

        int current_team_id;

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

        private string firstName;
        private string lastName;
        private int position;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        public NewPlayerViewModel(int team_id)
        {
            current_team_id = team_id;
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

            player_photo = result.FullPath.Replace("\\", "\\\\");
        }

        [RelayCommand]
        private void AddPlayer()
        {
            if (player_photo == null)
            {
                Application.Current.MainPage.DisplayAlert("ATTENTION", "You haven't picked a photo for the player yet!", "OK");
            }
            else
            {
                try
                {

                    int player_id;

                    sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                ";password=" + password +
                                                ";database=" + database +
                                                ";convert zero datetime=True";

                    sqlConn.Open();

                    String sql = "SELECT COUNT(*)+1 FROM `players`; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    player_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    player_fname = firstName;
                    player_lname = lastName;
                    player_position = position;

                    String sql2 = "INSERT INTO `players` (`player_id`, `first_name`, `last_name`, `team`, `photo`, `position`) " +
                        "VALUES (" + player_id + ", '" + player_fname + "', '" + player_lname + "', '" + current_team_id + "', '" + player_photo + "', '" + player_position + "');";

                    sqlCmd = new MySqlCommand(sql2, sqlConn);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    sqlRd2.Close();

                    sqlConn.Close();

                    Shell.Current.GoToAsync(nameof(TeamsPage));

                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                }
            }
        }


    }
}
