using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySql.Data.MySqlClient;

namespace MyLeagueApp.ViewModels
{
    partial class NewLeagueViewModel : ObservableObject
    {
        public string league_name;

        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        MySqlDataReader sqlRd;
        MySqlDataReader sqlRd2;
        MySqlDataReader sqlRd3;

        ViewCell lastCell;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        public NewLeagueViewModel()
        {

        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [RelayCommand]
        private void CreateLeague()
        {
            try
            {

                int league_id;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql = "SELECT league_id+1 FROM `leagues` ORDER BY league_id DESC LIMIT 0,1; ";

                sqlCmd = new MySqlCommand(sql, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();

                while (sqlRd.Read())
                {

                }

                league_id = Int32.Parse(sqlRd[0].ToString());
                sqlRd.Close();

                league_name = name;

                String sql2 = "INSERT INTO `leagues` (`name`, `created_date`) " +
                    "VALUES ('" + league_name + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "');";

                sqlCmd = new MySqlCommand(sql2, sqlConn);

                sqlRd2 = sqlCmd.ExecuteReader();

                while (sqlRd2.Read())
                {

                }

                sqlRd2.Close();

                String sql3 = "CREATE TABLE `myleague`.`" + league_name + "` (`m_id` INT(250) NOT NULL AUTO_INCREMENT , `home_team` INT(200) NOT NULL , `away_team` INT(200) NOT NULL , `home_score` INT(10) NOT NULL , `away_score` INT(10) NOT NULL , `date` DATE NOT NULL , PRIMARY KEY (`m_id`)) ENGINE = InnoDB;";

                sqlCmd = new MySqlCommand(sql3, sqlConn);

                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                sqlRd3.Close();

                sql3 = "CREATE TABLE `myleague`.`" + league_name + "_stats` (`stat_id` INT(250) NOT NULL AUTO_INCREMENT, " +
                    "`player_id` INT(250) NOT NULL, `match_id` INT(250) NOT NULL, `first_name` VARCHAR(250) NOT NULL," +
                    "`last_name` VARCHAR(250) NOT NULL, `points` INT(200) NOT NULL, " +
                    "`rebounds` INT(200) NOT NULL, `steals` INT(200) NOT NULL, " +
                    "`blocks` INT(200) NOT NULL, `threesmade` INT(200) NOT NULL, " +
                    "`threesattempted` INT(200) NOT NULL, `assists` INT(200) NOT NULL, " +
                    "PRIMARY KEY (`stat_id`)) ENGINE = InnoDB;";

                sqlCmd = new MySqlCommand(sql3, sqlConn);

                sqlRd3 = sqlCmd.ExecuteReader();

                while (sqlRd3.Read())
                {

                }

                sqlRd3.Close();

                sqlConn.Close();

                Application.Current.MainPage.DisplayAlert("", "Your new league has been created!", "OK");

                Shell.Current.GoToAsync(nameof(LeaguesPage));

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }
    }
}
