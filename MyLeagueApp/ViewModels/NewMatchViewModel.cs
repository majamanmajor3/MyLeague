using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyLeagueApp.Classes;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace MyLeagueApp.ViewModels
{
    partial class NewMatchViewModel : ObservableObject
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

        String current_league_name;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        ObservableCollection<Team> teams;

        [ObservableProperty]
        Team selected_home;

        [ObservableProperty]
        Team selected_away;

        [ObservableProperty]
        DateTime selected_date = DateTime.Now;

        public NewMatchViewModel(string league_name)
        {
            Teams = new ObservableCollection<Team>();
            current_league_name = league_name;
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
                sqlConn.Close();
            }

        }

        [RelayCommand]
        private void AddMatchClicked()
        {
            if (Selected_home == null || Selected_away == null)
            {
                Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("ATTENTION", "You haven't picked all data for the match yet!", "OK");
            }
            else
            {
                try
                {

                    int match_id;
                    int home_team;
                    int away_team;
                    DateTime date;

                    sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                ";password=" + password +
                                                ";database=" + database +
                                                ";convert zero datetime=True";

                    sqlConn.Open();

                    String sql = "SELECT COUNT(*)+1 FROM `" + current_league_name + "` ORDER BY m_id DESC LIMIT 0,1; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    match_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    date = Selected_date;

                    String sql2 = "INSERT INTO `" + current_league_name + "` (`home_team`, `away_team`, `home_score`, `away_score`, `date`) " +
                        "VALUES ('" + Selected_home.Id + "', '" + Selected_away.Id + "', '0', '0', '" + date.ToString("yyyy-MM-dd") + "');";

                    sqlCmd = new MySqlCommand(sql2, sqlConn);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    sqlRd2.Close();

                    sqlConn.Close();

                    Application.Current.MainPage.DisplayAlert("", "Your new match has been created!", "OK");

                    Shell.Current.GoToAsync(nameof(LeaguesPage));

                }
                catch (Exception ex)
                {
                    Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                    sqlConn.Close();
                }
            }
        }
    }
}
