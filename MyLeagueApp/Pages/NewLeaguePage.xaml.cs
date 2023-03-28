using MyLeagueApp.ViewModels;
using MySql.Data.MySqlClient;

namespace MyLeagueApp;

public partial class NewLeaguePage : ContentPage
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

    public NewLeaguePage()
    {
        InitializeComponent();
        BindingContext = new NewLeagueViewModel();
    }

    private void CreateLeagueClicked(object sender, EventArgs e)
    {
        try
        {

            int league_id;

            sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                        ";password=" + password +
                                        ";database=" + database +
                                        ";convert zero datetime=True";

            sqlConn.Open();

            String sql = "SELECT COUNT(*)+1 FROM `leagues`; ";

            sqlCmd = new MySqlCommand(sql, sqlConn);
            sqlRd = sqlCmd.ExecuteReader();

            while (sqlRd.Read())
            {

            }

            league_id = Int32.Parse(sqlRd[0].ToString());
            sqlRd.Close();

            league_name = name.Text;

            String sql2 = "INSERT INTO `leagues` (`league_id`, `name`, `created_date`) " +
                "VALUES (" + league_id + ", '" + league_name + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "');";

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

            sqlConn.Close();

            DisplayAlert("", "Your new league has been creted!", "OK");

            Navigation.PushAsync(new LeaguesPage());

        }
        catch (Exception ex)
        {
            DisplayAlert("", ex.Message, "OK");
        }
    }
}