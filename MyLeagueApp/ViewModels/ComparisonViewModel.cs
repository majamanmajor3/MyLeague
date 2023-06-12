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
    partial class ComparisonViewModel : ObservableObject
    {

        MySqlConnection sqlConn = new MySqlConnection();
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

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        [ObservableProperty]
        List<string> types;

        [ObservableProperty]
        ObservableCollection<PlayerSample> players;

        [ObservableProperty]
        ObservableCollection<TeamSample> teams;

        [ObservableProperty]
        ObservableCollection<string> seasons1;

        [ObservableProperty]
        ObservableCollection<string> seasons2;

        [ObservableProperty]
        SeasonalStats stats1;

        [ObservableProperty]
        SeasonalStats stats2;

        [ObservableProperty]
        SeasonalStatsTeam stats_team1;

        [ObservableProperty]
        SeasonalStatsTeam stats_team2;

        [ObservableProperty]
        string selected_type1;

        [ObservableProperty]
        string selected_type2;

        [ObservableProperty]
        PlayerSample selected_player1;

        [ObservableProperty]
        PlayerSample selected_player2;

        [ObservableProperty]
        TeamSample selected_team1;

        [ObservableProperty]
        TeamSample selected_team2;

        [ObservableProperty]
        string selected_season1;

        [ObservableProperty]
        string selected_season2;

        [ObservableProperty]
        bool pickerVisibility = false;

        [ObservableProperty]
        bool labelVisibility = false;

        [ObservableProperty]
        bool statsVisibility = false;

        [ObservableProperty]
        bool playersVisibility = false;

        [ObservableProperty]
        bool playerSeasonsVisibility = false;

        [ObservableProperty]
        bool teamSeasonsVisibility = false;

        [ObservableProperty]
        bool teamsVisibility = false;

        [ObservableProperty]
        bool teamStatsVisibility = false;

        [ObservableProperty]
        private Color pointsColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color pointsColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color reboundsColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color reboundsColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color assistsColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color assistsColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color stealsColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color stealsColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color blocksColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color blocksColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color fgmadeColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color fgmadeColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color fgattColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color fgattColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color threesmadeColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color threesmadeColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color threesattColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color threesattColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color teamPPGColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color teamPPGColor2= Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color teamAPPGColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color teamAPPGColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color teamWinsColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color teamWinsColor2 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color teamLossesColor1 = Color.FromRgb(0, 0, 0);

        [ObservableProperty]
        private Color teamLossesColor2 = Color.FromRgb(0, 0, 0);

        public ComparisonViewModel()
        {
            Types = new List<string>{"Player","Team"};
            Players = new ObservableCollection<PlayerSample>();
            Teams = new ObservableCollection<TeamSample>();
            Seasons1 = new ObservableCollection<string>();
            Seasons2 = new ObservableCollection<string>();
        }

        [RelayCommand]
        private void LoadElements()
        {
            try
            {
                if ((Selected_type1 != Selected_type2) || (Selected_type1==null) || (Selected_type2==null))
                {
                    LabelVisibility = true;
                    return;
                }
                else
                {
                    Players = new ObservableCollection<PlayerSample>();
                    Teams = new ObservableCollection<TeamSample>();

                    PlayersVisibility = false;
                    TeamsVisibility = false;
                    PlayerSeasonsVisibility = false;
                    TeamSeasonsVisibility = false;
                    StatsVisibility = false;

                    if (Selected_type1 == "Player")
                    {
                        LabelVisibility = false;

                        string player_count = "0";
                        int player_id;
                        int team_id;
                        string first_name;
                        string last_name;
                        string position;
                        string height_feet;
                        string height_inches;
                        string weight;

                        sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                    ";password=" + password +
                                                    ";database=" + database +
                                                    ";convert zero datetime=True";

                        sqlConn.Open();

                        String sql = "SELECT COUNT(*) FROM `sampled_players`; ";

                        sqlCmd = new MySqlCommand(sql, sqlConn);
                        sqlRd3 = sqlCmd.ExecuteReader();

                        while (sqlRd3.Read())
                        {

                        }

                        player_count = sqlRd3[0].ToString();
                        sqlRd3.Close();
                        sqlConn.Close();

                        int cr = Int32.Parse(player_count);

                        if (cr == 0)
                        {
                            //label.IsVisible = true;
                        }

                        for (int i = 0; i < cr; i++)
                        {
                            sqlConn.Open();

                            String sql_m_id = "SELECT player_id FROM sampled_players LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                            sqlRd = sqlCmd.ExecuteReader();

                            while (sqlRd.Read())
                            {

                            }

                            player_id = Int32.Parse(sqlRd[0].ToString());
                            sqlRd.Close();

                            String sql_t_id = "SELECT team FROM sampled_players LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_t_id, sqlConn);
                            sqlRd = sqlCmd.ExecuteReader();

                            while (sqlRd.Read())
                            {

                            }

                            team_id = Int32.Parse(sqlRd[0].ToString());
                            sqlRd.Close();

                            String sql_h_id = "SELECT first_name FROM sampled_players LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                            sqlRd2 = sqlCmd.ExecuteReader();

                            while (sqlRd2.Read())
                            {

                            }

                            first_name = sqlRd2[0].ToString();
                            sqlRd2.Close();

                            String sql_a_id = "SELECT last_name FROM sampled_players LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                            sqlRd4 = sqlCmd.ExecuteReader();

                            while (sqlRd4.Read())
                            {

                            }

                            last_name = sqlRd4[0].ToString();
                            sqlRd4.Close();

                            String sql_pos = "SELECT position FROM sampled_players LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_pos, sqlConn);
                            sqlRd6 = sqlCmd.ExecuteReader();

                            while (sqlRd6.Read())
                            {

                            }

                            position = sqlRd6[0].ToString();
                            sqlRd6.Close();

                            String sql_hef = "SELECT height_feet FROM sampled_players LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_hef, sqlConn);
                            sqlRd6 = sqlCmd.ExecuteReader();

                            while (sqlRd6.Read())
                            {

                            }

                            height_feet = sqlRd6[0].ToString();
                            sqlRd6.Close();

                            String sql_hei = "SELECT height_inches FROM sampled_players LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_hei, sqlConn);
                            sqlRd6 = sqlCmd.ExecuteReader();

                            while (sqlRd6.Read())
                            {

                            }

                            height_inches = sqlRd6[0].ToString();
                            sqlRd6.Close();

                            String sql_we = "SELECT weight_pounds FROM sampled_players LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_we, sqlConn);
                            sqlRd6 = sqlCmd.ExecuteReader();

                            while (sqlRd6.Read())
                            {

                            }

                            weight = sqlRd6[0].ToString();
                            sqlRd6.Close();

                            sqlConn.Close();

                            string full_name = first_name + " " + last_name;

                            string pos_name = "NA";

                            if (position == "G") pos_name = "Guard";
                            if (position == "PG") pos_name = "Point Guard";
                            if (position == "SG") pos_name = "Shooting Guard";
                            if (position == "F") pos_name = "Forward";
                            if (position == "SF") pos_name = "Small Forward";
                            if (position == "PF") pos_name = "Power Forward";
                            if (position == "C") pos_name = "Center";

                            Players.Add(new PlayerSample(player_id, first_name, last_name, team_id, position, pos_name, height_feet, height_inches, weight, full_name));

                            PlayersVisibility = true;
                        }
                    }
                    else
                    {
                        LabelVisibility = false;

                        string team_count = "0";
                        int team_id;
                        int api_id;
                        string name;
                        string city;
                        string abbreviation;
                        string division;
                        string conference;

                        sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                    ";password=" + password +
                                                    ";database=" + database +
                                                    ";convert zero datetime=True";

                        sqlConn.Open();

                        String sql = "SELECT COUNT(*) FROM `sampled_teams`; ";

                        sqlCmd = new MySqlCommand(sql, sqlConn);
                        sqlRd3 = sqlCmd.ExecuteReader();

                        while (sqlRd3.Read())
                        {

                        }

                        team_count = sqlRd3[0].ToString();
                        sqlRd3.Close();
                        sqlConn.Close();

                        int cr = Int32.Parse(team_count);

                        if (cr == 0)
                        {
                            //label.IsVisible = true;
                        }

                        for (int i = 0; i < cr; i++)
                        {
                            sqlConn.Open();

                            String sql_m_id = "SELECT team_id FROM sampled_teams LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                            sqlRd = sqlCmd.ExecuteReader();

                            while (sqlRd.Read())
                            {

                            }

                            team_id = Int32.Parse(sqlRd[0].ToString());
                            sqlRd.Close();

                            String sql_t_id = "SELECT api_id FROM sampled_teams LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_t_id, sqlConn);
                            sqlRd = sqlCmd.ExecuteReader();

                            while (sqlRd.Read())
                            {

                            }

                            api_id = Int32.Parse(sqlRd[0].ToString());
                            sqlRd.Close();

                            String sql_h_id = "SELECT city FROM sampled_teams LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                            sqlRd2 = sqlCmd.ExecuteReader();

                            while (sqlRd2.Read())
                            {

                            }

                            city = sqlRd2[0].ToString();
                            sqlRd2.Close();

                            String sql_a_id = "SELECT name FROM sampled_teams LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                            sqlRd4 = sqlCmd.ExecuteReader();

                            while (sqlRd4.Read())
                            {

                            }

                            name = sqlRd4[0].ToString();
                            sqlRd4.Close();

                            String sql_pos = "SELECT abbreviation FROM sampled_teams LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_pos, sqlConn);
                            sqlRd6 = sqlCmd.ExecuteReader();

                            while (sqlRd6.Read())
                            {

                            }

                            abbreviation = sqlRd6[0].ToString();
                            sqlRd6.Close();

                            String sql_hef = "SELECT conference FROM sampled_teams LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_hef, sqlConn);
                            sqlRd6 = sqlCmd.ExecuteReader();

                            while (sqlRd6.Read())
                            {

                            }

                            conference = sqlRd6[0].ToString();
                            sqlRd6.Close();

                            String sql_hei = "SELECT division FROM sampled_teams LIMIT " + i + ",1; ";

                            sqlCmd = new MySqlCommand(sql_hei, sqlConn);
                            sqlRd6 = sqlCmd.ExecuteReader();

                            while (sqlRd6.Read())
                            {

                            }

                            division = sqlRd6[0].ToString();
                            sqlRd6.Close();

                            sqlConn.Close();

                            string full_name = city + " " + name;

                            Teams.Add(new TeamSample(team_id, api_id, name, city, abbreviation, division, conference, full_name));

                            TeamsVisibility = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

        [RelayCommand]
        private void LoadPlayerSeasons()
        {
            try
            {
                if ((Selected_player1 == Selected_player2) || (Selected_player1 == null) || (Selected_player2 == null))
                {
                    LabelVisibility = true;
                    return;
                }
                else
                {
                    Seasons1 = new ObservableCollection<string>();
                    Seasons2 = new ObservableCollection<string>();

                    LabelVisibility = false;

                    string seasons_count = "0";
                    string season;

                    sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                ";password=" + password +
                                                ";database=" + database +
                                                ";convert zero datetime=True";

                    sqlConn.Open();

                    String sql = "SELECT COUNT(*) FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    seasons_count = sqlRd3[0].ToString();
                    sqlRd3.Close();
                    //sqlConn.Close();

                    int cr = Int32.Parse(seasons_count);

                    if (cr == 0)
                    {
                        //label.IsVisible = true;
                    }

                    for (int i = 0; i < cr; i++)
                    {
                        //sqlConn.Open();

                        String sql_m_id = "SELECT season FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " LIMIT " + i + ",1; ";

                        sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        season = sqlRd[0].ToString();
                        sqlRd.Close();

                        Seasons1.Add(season);
                    }

                    sql = "SELECT COUNT(*) FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    seasons_count = sqlRd3[0].ToString();
                    sqlRd3.Close();
                    //sqlConn.Close();

                    cr = Int32.Parse(seasons_count);

                    if (cr == 0)
                    {
                        //label.IsVisible = true;
                    }

                    for (int i = 0; i < cr; i++)
                    {
                        //sqlConn.Open();

                        String sql_m_id = "SELECT season FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " LIMIT " + i + ",1; ";

                        sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        season = sqlRd[0].ToString();
                        sqlRd.Close();

                        Seasons2.Add(season);
                    }

                    sqlConn.Close();

                    PlayerSeasonsVisibility = true;
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

        [RelayCommand]
        private void LoadTeamSeasons()
        {
            try
            {
                if ((Selected_team1 == Selected_team2) || (Selected_team1 == null) || (Selected_team2 == null))
                {
                    LabelVisibility = true;
                    return;
                }
                else
                {
                    LabelVisibility = false;

                    Seasons1 = new ObservableCollection<string>();
                    Seasons2 = new ObservableCollection<string>();

                    string seasons_count = "0";
                    string season;

                    sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                ";password=" + password +
                                                ";database=" + database +
                                                ";convert zero datetime=True";

                    sqlConn.Open();

                    String sql = "SELECT COUNT(*) FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team1.ApiId + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    seasons_count = sqlRd3[0].ToString();
                    sqlRd3.Close();
                    //sqlConn.Close();

                    int cr = Int32.Parse(seasons_count);

                    if (cr == 0)
                    {
                        //label.IsVisible = true;
                    }

                    for (int i = 0; i < cr; i++)
                    {
                        //sqlConn.Open();

                        String sql_m_id = "SELECT season FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team1.ApiId + " LIMIT " + i + ",1; ";

                        sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        season = sqlRd[0].ToString();
                        sqlRd.Close();

                        Seasons1.Add(season);
                    }

                    sql = "SELECT COUNT(*) FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team2.ApiId + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    seasons_count = sqlRd3[0].ToString();
                    sqlRd3.Close();
                    //sqlConn.Close();

                    cr = Int32.Parse(seasons_count);

                    if (cr == 0)
                    {
                        //label.IsVisible = true;
                    }

                    for (int i = 0; i < cr; i++)
                    {
                        //sqlConn.Open();

                        String sql_m_id = "SELECT season FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team2.ApiId + " LIMIT " + i + ",1; ";

                        sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        season = sqlRd[0].ToString();
                        sqlRd.Close();

                        Seasons2.Add(season);
                    }

                    sqlConn.Close();

                    TeamSeasonsVisibility = true;
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

        [RelayCommand]
        private void LoadPlayerStatistics()
        {
            try
            {
                if ((Selected_season1 == null) || (Selected_season2 == null))
                {
                    LabelVisibility = true;
                    return;
                }
                else
                {
                    LabelVisibility = false;

                    string stats_count = "0";
                    int season_id;
                    double points;
                    double rebounds;
                    double assists;
                    double steals;
                    double blocks;
                    double fgmade;
                    double fgatt;
                    double threesmade;
                    double threesatt;

                    sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                ";password=" + password +
                                                ";database=" + database +
                                                ";convert zero datetime=True";

                    sqlConn.Open();

                    String sql = "SELECT COUNT(*) FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    stats_count = sqlRd3[0].ToString();
                    sqlRd3.Close();
                    //sqlConn.Close();

                    int cr = Int32.Parse(stats_count);

                    if (cr == 0)
                    {
                        //label.IsVisible = true;
                    }

                    for (int i = 0; i < cr; i++)
                    {
                        //sqlConn.Open();

                        String sql_id = "SELECT season_id FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_id, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        season_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_pts = "SELECT points FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_pts, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        points = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_rb = "SELECT rebounds FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_rb, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        rebounds = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_as = "SELECT assists FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_as, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        assists = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_st = "SELECT steals FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_st, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        steals = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_bl = "SELECT blocks FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_bl, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        blocks = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_fgm = "SELECT fg_made FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_fgm, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        fgmade = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_fga = "SELECT fg_attempted FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_fga, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        fgatt = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_tm = "SELECT threes_made FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_tm, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        threesmade = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_ta = "SELECT threes_attempted FROM `seasonal_stats` WHERE `player_id`=" + Selected_player1.Id + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_ta, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        threesatt = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        Stats1 = new SeasonalStats(season_id, Selected_player1.Id, points, rebounds, assists, steals, blocks, fgmade, fgatt, threesmade, threesatt, Int32.Parse(Selected_season1));
                    }



                    sql = "SELECT COUNT(*) FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    stats_count = sqlRd3[0].ToString();
                    sqlRd3.Close();
                    //sqlConn.Close();

                    cr = Int32.Parse(stats_count);

                    if (cr == 0)
                    {
                        //label.IsVisible = true;
                    }

                    for (int i = 0; i < cr; i++)
                    {
                        //sqlConn.Open();

                        String sql_id = "SELECT season_id FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_id, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        season_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_pts = "SELECT points FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_pts, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        points = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_rb = "SELECT rebounds FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_rb, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        rebounds = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_as = "SELECT assists FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_as, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        assists = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_st = "SELECT steals FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_st, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        steals = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_bl = "SELECT blocks FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_bl, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        blocks = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_fgm = "SELECT fg_made FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_fgm, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        fgmade = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_fga = "SELECT fg_attempted FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_fga, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        fgatt = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_tm = "SELECT threes_made FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_tm, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        threesmade = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_ta = "SELECT threes_attempted FROM `seasonal_stats` WHERE `player_id`=" + Selected_player2.Id + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_ta, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        threesatt = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        Stats2 = new SeasonalStats(season_id, Selected_player2.Id, points, rebounds, assists, steals, blocks, fgmade, fgatt, threesmade, threesatt, Int32.Parse(Selected_season2));
                    }

                    StatsVisibility = true;
                    sqlConn.Close();

                    if(Stats1.Points > Stats2.Points)
                    {
                        PointsColor1 = Color.FromRgb(0, 128, 0);
                        PointsColor2 = Color.FromRgb(255, 0, 0);
                    } else if(Stats1.Points < Stats2.Points)
                    {
                        PointsColor1 = Color.FromRgb(255, 0, 0);
                        PointsColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats1.Rebounds > Stats2.Rebounds)
                    {
                        ReboundsColor1 = Color.FromRgb(0, 128, 0);
                        ReboundsColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats1.Rebounds < Stats2.Rebounds)
                    {
                        ReboundsColor1 = Color.FromRgb(255, 0, 0);
                        ReboundsColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats1.Assists > Stats2.Assists)
                    {
                        AssistsColor1 = Color.FromRgb(0, 128, 0);
                        AssistsColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats1.Assists < Stats2.Assists)
                    {
                        AssistsColor1 = Color.FromRgb(255, 0, 0);
                        AssistsColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats1.Steals > Stats2.Steals)
                    {
                        StealsColor1 = Color.FromRgb(0, 128, 0);
                        StealsColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats1.Steals < Stats2.Steals)
                    {
                        StealsColor1 = Color.FromRgb(255, 0, 0);
                        StealsColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats1.Blocks > Stats2.Blocks)
                    {
                        BlocksColor1 = Color.FromRgb(0, 128, 0);
                        BlocksColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats1.Blocks < Stats2.Blocks)
                    {
                        BlocksColor1 = Color.FromRgb(255, 0, 0);
                        BlocksColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats1.FGMade > Stats2.FGMade)
                    {
                        FgmadeColor1= Color.FromRgb(0, 128, 0);
                        FgmadeColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats1.FGMade < Stats2.FGMade)
                    {
                        FgmadeColor1 = Color.FromRgb(255, 0, 0);
                        FgmadeColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats1.FGAttempted > Stats2.FGAttempted)
                    {
                        FgattColor1 = Color.FromRgb(0, 128, 0);
                        FgattColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats1.FGAttempted < Stats2.FGAttempted)
                    {
                        FgattColor1 = Color.FromRgb(255, 0, 0);
                        FgattColor1 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats1.ThreesMade > Stats2.ThreesMade)
                    {
                        ThreesmadeColor1 = Color.FromRgb(0, 128, 0);
                        ThreesmadeColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats1.ThreesMade < Stats2.ThreesMade)
                    {
                        ThreesmadeColor1 = Color.FromRgb(255, 0, 0);
                        ThreesmadeColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats1.ThreesAttempted > Stats2.ThreesAttempted)
                    {
                        ThreesattColor1 = Color.FromRgb(0, 128, 0);
                        ThreesattColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats1.ThreesAttempted < Stats2.ThreesAttempted)
                    {
                        ThreesattColor1 = Color.FromRgb(255, 0, 0);
                        ThreesattColor2 = Color.FromRgb(0, 128, 0);
                    }

                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

        [RelayCommand]
        private void LoadTeamStatistics()
        {
            try
            {
                if ((Selected_season1 == null) || (Selected_season2 == null))
                {
                    LabelVisibility = true;
                    return;
                }
                else
                {
                    LabelVisibility = false;

                    string stats_count = "0";
                    int season_id;
                    double ppg;
                    double appg;
                    int wins;
                    int losses;

                    sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                                ";password=" + password +
                                                ";database=" + database +
                                                ";convert zero datetime=True";

                    sqlConn.Open();

                    String sql = "SELECT COUNT(*) FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team1.ApiId + " AND `season`=" + Selected_season1 + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    stats_count = sqlRd3[0].ToString();
                    sqlRd3.Close();
                    //sqlConn.Close();

                    int cr = Int32.Parse(stats_count);

                    if (cr == 0)
                    {
                        //label.IsVisible = true;
                    }

                    for (int i = 0; i < cr; i++)
                    {
                        //sqlConn.Open();

                        String sql_id = "SELECT stat_id FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team1.ApiId + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_id, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        season_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_pts = "SELECT wins FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team1.ApiId + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_pts, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        wins = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_rb = "SELECT losses FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team1.ApiId + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_rb, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        losses = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_as = "SELECT ppg FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team1.ApiId + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_as, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        ppg = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_st = "SELECT appg FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team1.ApiId + " AND `season`=" + Selected_season1 + "; ";

                        sqlCmd = new MySqlCommand(sql_st, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        appg = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        Stats_team1 = new SeasonalStatsTeam(season_id, Selected_team1.ApiId, wins, losses, ppg, appg, Int32.Parse(Selected_season1));
                    }



                    sql = "SELECT COUNT(*) FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team2.ApiId + " AND `season`=" + Selected_season2 + "; ";

                    sqlCmd = new MySqlCommand(sql, sqlConn);
                    sqlRd3 = sqlCmd.ExecuteReader();

                    while (sqlRd3.Read())
                    {

                    }

                    stats_count = sqlRd3[0].ToString();
                    sqlRd3.Close();
                    //sqlConn.Close();

                    cr = Int32.Parse(stats_count);

                    if (cr == 0)
                    {
                        //label.IsVisible = true;
                    }

                    for (int i = 0; i < cr; i++)
                    {
                        //sqlConn.Open();

                        String sql_id = "SELECT stat_id FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team2.ApiId + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_id, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        season_id = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_pts = "SELECT wins FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team2.ApiId + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_pts, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        wins = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_rb = "SELECT losses FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team2.ApiId + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_rb, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        losses = Int32.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_as = "SELECT ppg FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team2.ApiId + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_as, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        ppg = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        String sql_st = "SELECT appg FROM `seasonal_stats_teams` WHERE `team_id`=" + Selected_team2.ApiId + " AND `season`=" + Selected_season2 + "; ";

                        sqlCmd = new MySqlCommand(sql_st, sqlConn);
                        sqlRd = sqlCmd.ExecuteReader();

                        while (sqlRd.Read())
                        {

                        }

                        appg = Double.Parse(sqlRd[0].ToString());
                        sqlRd.Close();

                        Stats_team2 = new SeasonalStatsTeam(season_id, Selected_team2.ApiId, wins, losses, ppg, appg, Int32.Parse(Selected_season2));
                    }

                    TeamStatsVisibility = true;
                    sqlConn.Close();

                    if (Stats_team1.Wins > Stats_team2.Wins)
                    {
                        TeamWinsColor1 = Color.FromRgb(0, 128, 0);
                        TeamWinsColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats_team1.Wins < Stats_team2.Wins)
                    {
                        TeamWinsColor1 = Color.FromRgb(255, 0, 0);
                        TeamWinsColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats_team1.Losses < Stats_team2.Losses)
                    {
                        TeamLossesColor1 = Color.FromRgb(0, 128, 0);
                        TeamLossesColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats_team1.Losses > Stats_team2.Losses)
                    {
                        TeamLossesColor1 = Color.FromRgb(255, 0, 0);
                        TeamLossesColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats_team1.PPG > Stats_team2.PPG)
                    {
                        TeamPPGColor1 = Color.FromRgb(0, 128, 0);
                        TeamPPGColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats_team1.PPG < Stats_team2.PPG)
                    {
                        TeamPPGColor1 = Color.FromRgb(255, 0, 0);
                        TeamPPGColor2 = Color.FromRgb(0, 128, 0);
                    }

                    if (Stats_team1.APPG < Stats_team2.APPG)
                    {
                        TeamAPPGColor1 = Color.FromRgb(0, 128, 0);
                        TeamAPPGColor2 = Color.FromRgb(255, 0, 0);
                    }
                    else if (Stats_team1.APPG > Stats_team2.APPG)
                    {
                        TeamAPPGColor1 = Color.FromRgb(255, 0, 0);
                        TeamAPPGColor2 = Color.FromRgb(0, 128, 0);
                    }

                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                sqlConn.Close();
            }
        }

    }
}
