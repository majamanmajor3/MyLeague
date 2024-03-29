﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyLeagueApp.Classes;
using MyLeagueApp.Classes.Stats;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeagueApp.ViewModels
{
    partial class MatchOverviewViewModel : ObservableObject
    {
        MySqlConnection sqlConn = new MySqlConnection();
        MySqlConnection sqlConn2 = new MySqlConnection();
        MySqlConnection sqlConn3 = new MySqlConnection();
        MySqlConnection sqlConn4 = new MySqlConnection();
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
        MySqlDataReader sqlRdHS1;
        MySqlDataReader sqlRdDate;

        String server = "localhost";
        String username = "root";
        String password = "";
        String database = "myleague";

        String current_league_name;
        int current_match_id;
        int hometeam;
        int awayteam;

        [ObservableProperty]
        ObservableCollection<Team> teams;

        [ObservableProperty]
        private int homeScore;

        [ObservableProperty]
        private int awayScore;

        [ObservableProperty]
        private string matchDate;

        [ObservableProperty]
        private Team homeTeam;

        [ObservableProperty]
        private Team awayTeam;

        [ObservableProperty]
        private Player starPlayerHome;

        [ObservableProperty]
        private Player starPlayerAway;

        [ObservableProperty]
        private PlayerStat starPlayerStatsHome;

        [ObservableProperty]
        private PlayerStat starPlayerStatsAway;

        [ObservableProperty]
        private string homePoints;

        [ObservableProperty]
        private string awayPoints;

        [ObservableProperty]
        private string homeRebounds;

        [ObservableProperty]
        private string awayRebounds;

        [ObservableProperty]
        private string homeAssists;

        [ObservableProperty]
        private string awayAssists;

        [ObservableProperty]
        private Arena arena;

        [ObservableProperty]
        private string arenaLocation;

        [ObservableProperty]
        private string matchup;

        [ObservableProperty]
        private bool noPlayerVisibility1 = false;

        [ObservableProperty]
        private bool noPlayerVisibility2 = false;

        public MatchOverviewViewModel(string league_name, int match_id)
        {
            Teams = new ObservableCollection<Team>();
            current_match_id = match_id;
            current_league_name = league_name;
            loadTeams();
            loadSelections();
            loadArena(HomeTeam.Id);
            loadStarPlayers(HomeTeam.Id, AwayTeam.Id);
            loadStarPlayersStats(starPlayerHome.Id, starPlayerAway.Id);
            Matchup = HomeTeam.Name + " vs. " + AwayTeam.Name;
            if (StarPlayerStatsHome.Points != 0 || StarPlayerStatsAway.Points != 0)
            {
                if (StarPlayerStatsHome.Points != 0) HomePoints = StarPlayerStatsHome.Points + " PPG";
                if (StarPlayerStatsAway.Points != 0) AwayPoints = StarPlayerStatsAway.Points + " PPG";
                if (StarPlayerStatsHome.Rebounds != 0) HomeRebounds = StarPlayerStatsHome.Rebounds + " RPG";
                if (StarPlayerStatsAway.Rebounds != 0) AwayRebounds = StarPlayerStatsAway.Rebounds + " RPG";
                if (StarPlayerStatsHome.Assists != 0) HomeAssists = StarPlayerStatsHome.Assists + " APG";
                if (StarPlayerStatsAway.Assists != 0) AwayAssists = StarPlayerStatsAway.Assists + " APG";
            }
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

                List<Team> list = Teams.ToList();

                for(int i=0; i<list.Count; i++)
                {
                    if (list[i].Id == home_team) HomeTeam = list[i];
                    if (list[i].Id == away_team) AwayTeam = list[i];
                }

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

                MatchDate = sqlRdDate[0].ToString();
                sqlRdDate.Close();

                sqlConn2.Close();

                HomeScore = home_score;
                AwayScore = away_score;

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }

        }

        private void loadArena(int team_id)
        {
            try
            {

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                int arena_id;
                string arena_name;
                string arena_city;
                string arena_state;
                double arena_latitude;
                double arena_longitude;

                String sqlArena = "SELECT COUNT(*) FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena, sqlConn);
                sqlRdA1 = sqlCmd.ExecuteReader();

                while (sqlRdA1.Read())
                {

                }

                int check = Int32.Parse(sqlRdA1[0].ToString());

                sqlRdA1.Close();

                if(check == 0)
                {
                    Arena = new Arena(0, "No Specified Arena", "", "", 0, 0, team_id);
                    sqlConn.Close();
                    return;
                }

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

                arena_name = sqlRdA2[0].ToString();

                sqlRdA2.Close();

                String sqlArena3 = "SELECT city FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena3, sqlConn);
                sqlRdA3 = sqlCmd.ExecuteReader();

                while (sqlRdA3.Read())
                {

                }

                arena_city = sqlRdA3[0].ToString();

                sqlRdA3.Close();

                String sqlArena4 = "SELECT state FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena4, sqlConn);
                sqlRdA4 = sqlCmd.ExecuteReader();

                while (sqlRdA4.Read())
                {

                }

                arena_state = sqlRdA4[0].ToString();

                sqlRdA4.Close();

                String sqlArena5 = "SELECT latitude FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena5, sqlConn);
                sqlRdA5 = sqlCmd.ExecuteReader();

                while (sqlRdA5.Read())
                {

                }

                arena_latitude = Double.Parse(sqlRdA5[0].ToString());

                sqlRdA5.Close();

                String sqlArena6 = "SELECT longitude FROM `arenas` WHERE `team_id`=" + team_id + ";";

                sqlCmd = new MySqlCommand(sqlArena6, sqlConn);
                sqlRdA6 = sqlCmd.ExecuteReader();

                while (sqlRdA6.Read())
                {

                }

                arena_longitude = Double.Parse(sqlRdA6[0].ToString());

                sqlRdA6.Close();

                sqlConn.Close();

                ArenaLocation = arena_city + ", " + arena_state;

                Arena = new Arena(arena_id, arena_name, arena_city, arena_state, arena_latitude, arena_longitude, team_id);

            }
            catch (Exception ex)
            {
                //DisplayAlert("", ex.Message, "OK");
            }

        }

        private void loadStarPlayers(int home_id, int away_id)
        {
            try
            {

                int player_id;
                string first_name;
                string last_name;
                string photo;
                int position;

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql_check = "SELECT COUNT(*) FROM players WHERE `team`=" + home_id + "; ";

                sqlCmd = new MySqlCommand(sql_check, sqlConn);
                sqlRdHS1 = sqlCmd.ExecuteReader();

                while (sqlRdHS1.Read())
                {

                }

                int check = Int32.Parse(sqlRdHS1[0].ToString());
                sqlRdHS1.Close();

                if(check != 0)
                {
                    String sql_m_id = "SELECT player_id FROM players WHERE `team`=" + home_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    player_id = Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_h_id = "SELECT first_name FROM players WHERE `team`=" + home_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    first_name = sqlRd2[0].ToString();
                    sqlRd2.Close();

                    String sql_a_id = "SELECT last_name FROM players WHERE `team`=" + home_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                    sqlRd4 = sqlCmd.ExecuteReader();

                    while (sqlRd4.Read())
                    {

                    }

                    last_name = sqlRd4[0].ToString();
                    sqlRd4.Close();

                    String sql_h_score = "SELECT photo FROM players WHERE `team`=" + home_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_h_score, sqlConn);
                    sqlRd5 = sqlCmd.ExecuteReader();

                    while (sqlRd5.Read())
                    {

                    }

                    photo = sqlRd5[0].ToString();
                    sqlRd5.Close();

                    String sql_pos = "SELECT position FROM players WHERE `team`=" + home_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_pos, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    position = Int32.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    string full_name = first_name + " " + last_name;

                    string pos_name = "NA";

                    if (position == 1) pos_name = "Point Guard";
                    if (position == 2) pos_name = "Shooting Guard";
                    if (position == 3) pos_name = "Small Forward";
                    if (position == 4) pos_name = "Power Forward";
                    if (position == 5) pos_name = "Center";

                    StarPlayerHome = new Player(player_id, first_name, last_name, home_id, photo, position, pos_name, full_name);
                }
                else
                {
                    StarPlayerHome = new Player(0, "", "", 0, "", 0, "", "");
                }

                sql_check = "SELECT COUNT(*) FROM players WHERE `team`=" + away_id + "; ";

                sqlCmd = new MySqlCommand(sql_check, sqlConn);
                sqlRdHS1 = sqlCmd.ExecuteReader();

                while (sqlRdHS1.Read())
                {

                }

                check = Int32.Parse(sqlRdHS1[0].ToString());
                sqlRdHS1.Close();

                if(check != 0)
                {
                    String sql_m_id = "SELECT player_id FROM players WHERE `team`=" + away_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                    sqlRd = sqlCmd.ExecuteReader();

                    while (sqlRd.Read())
                    {

                    }

                    player_id = Int32.Parse(sqlRd[0].ToString());
                    sqlRd.Close();

                    String sql_h_id = "SELECT first_name FROM players WHERE `team`=" + away_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_h_id, sqlConn);
                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    first_name = sqlRd2[0].ToString();
                    sqlRd2.Close();

                    String sql_a_id = "SELECT last_name FROM players WHERE `team`=" + away_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                    sqlRd4 = sqlCmd.ExecuteReader();

                    while (sqlRd4.Read())
                    {

                    }

                    last_name = sqlRd4[0].ToString();
                    sqlRd4.Close();

                    String sql_h_score = "SELECT photo FROM players WHERE `team`=" + away_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_h_score, sqlConn);
                    sqlRd5 = sqlCmd.ExecuteReader();

                    while (sqlRd5.Read())
                    {

                    }

                    photo = sqlRd5[0].ToString();
                    sqlRd5.Close();

                    String sql_pos = "SELECT position FROM players WHERE `team`=" + away_id + " LIMIT 1; ";

                    sqlCmd = new MySqlCommand(sql_pos, sqlConn);
                    sqlRd6 = sqlCmd.ExecuteReader();

                    while (sqlRd6.Read())
                    {

                    }

                    position = Int32.Parse(sqlRd6[0].ToString());
                    sqlRd6.Close();

                    string full_name = first_name + " " + last_name;

                    string pos_name = "NA";

                    if (position == 1) pos_name = "Point Guard";
                    if (position == 2) pos_name = "Shooting Guard";
                    if (position == 3) pos_name = "Small Forward";
                    if (position == 4) pos_name = "Power Forward";
                    if (position == 5) pos_name = "Center";

                    StarPlayerAway = new Player(player_id, first_name, last_name, away_id, photo, position, pos_name, full_name);
                }
                else
                {
                    StarPlayerAway = new Player(0, "", "", 0, "", 0, "", "");
                }

                sqlConn.Close();

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        private void loadStarPlayersStats(int home_id, int away_id)
        {
            try
            {

                int player_id;
                string first_name;
                string last_name;
                string photo;
                int position;

                int home_gamecount;
                int away_gamecount;
                int total_points = 0;
                double average_points;
                int total_rebounds = 0;
                double average_rebounds;
                int total_assists = 0;
                double average_assists;
                int total_steals = 0;
                double average_steals;
                int total_blocks = 0;
                double average_blocks;
                int total_threesmade = 0;
                double average_threesmade;
                int total_threesatt = 0;
                double average_threesatt;

                StarPlayerStatsHome = new PlayerStat(0, 0, "", "", 0, 0, 0, 0, 0, 0, 0);
                StarPlayerStatsAway = new PlayerStat(0, 0, "", "", 0, 0, 0, 0, 0, 0, 0);

                sqlConn.ConnectionString = "server=" + server + ";user id=" + username +
                                            ";password=" + password +
                                            ";database=" + database +
                                            ";convert zero datetime=True";

                sqlConn.Open();

                String sql_m_id = "SELECT COUNT(*) FROM `" + current_league_name + "_stats` WHERE `player_id`=" + home_id + "; ";

                sqlCmd = new MySqlCommand(sql_m_id, sqlConn);
                sqlRdHS1 = sqlCmd.ExecuteReader();

                while (sqlRdHS1.Read())
                {

                }

                home_gamecount = Int32.Parse(sqlRdHS1[0].ToString());
                sqlRdHS1.Close();

                for (int i = 0; i < home_gamecount; i++)
                {

                    String sql_points = "SELECT points FROM `" + current_league_name + "_stats` WHERE `player_id`=" + home_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_points, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_points = total_points + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_rebounds = "SELECT rebounds FROM `" + current_league_name + "_stats` WHERE `player_id`=" + home_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_rebounds, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_rebounds = total_rebounds + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_assists = "SELECT assists FROM `" + current_league_name + "_stats` WHERE `player_id`=" + home_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_assists, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_assists = total_assists + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_steals = "SELECT steals FROM `" + current_league_name + "_stats` WHERE `player_id`=" + home_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_steals, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_steals = total_steals + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_blocks = "SELECT blocks FROM `" + current_league_name + "_stats` WHERE `player_id`=" + home_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_blocks, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_blocks = total_blocks + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_tmade = "SELECT threesmade FROM `" + current_league_name + "_stats` WHERE `player_id`=" + home_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_tmade, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_threesmade = total_threesmade + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_tatt = "SELECT threesattempted FROM `" + current_league_name + "_stats` WHERE `player_id`=" + home_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_tatt, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_threesatt = total_threesatt + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                }

                if (home_gamecount == 0)
                {
                    StarPlayerStatsHome = new PlayerStat(home_id, current_match_id, StarPlayerHome.FirstName, StarPlayerHome.LastName, 0, 0, 0, 0, 0, 0, 0);
                }
                else
                {
                    average_points = total_points / home_gamecount;
                    average_rebounds = total_rebounds / home_gamecount;
                    average_assists = total_assists / home_gamecount;
                    average_steals = total_steals / home_gamecount;
                    average_blocks = total_blocks / home_gamecount;
                    average_threesmade = total_threesmade / home_gamecount;
                    average_threesatt = total_threesatt / home_gamecount;

                    StarPlayerStatsHome = new PlayerStat(home_id, current_match_id, StarPlayerHome.FirstName, StarPlayerHome.LastName, average_points, average_rebounds, average_assists, average_steals, average_blocks, average_threesmade, average_threesatt);
                }

                total_points = 0;
                average_points = 0;
                total_rebounds = 0;
                average_rebounds = 0;
                total_assists = 0;
                average_assists = 0;
                total_steals = 0;
                average_steals = 0;
                total_blocks = 0;
                average_blocks = 0;
                total_threesmade = 0;
                average_threesmade = 0;
                total_threesatt = 0;
                average_threesatt = 0;


                String sql_a_id = "SELECT COUNT(*) FROM `" + current_league_name + "_stats` WHERE `player_id`=" + away_id + "; ";

                sqlCmd = new MySqlCommand(sql_a_id, sqlConn);
                sqlRdHS1 = sqlCmd.ExecuteReader();

                while (sqlRdHS1.Read())
                {

                }

                away_gamecount = Int32.Parse(sqlRdHS1[0].ToString());
                sqlRdHS1.Close();

                for (int i = 0; i < away_gamecount; i++)
                {

                    String sql_points = "SELECT points FROM `" + current_league_name + "_stats` WHERE `player_id`=" + away_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_points, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_points = total_points + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_rebounds = "SELECT rebounds FROM `" + current_league_name + "_stats` WHERE `player_id`=" + away_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_rebounds, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_rebounds = total_rebounds + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_assists = "SELECT assists FROM `" + current_league_name + "_stats` WHERE `player_id`=" + away_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_assists, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_assists = total_assists + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_steals = "SELECT steals FROM `" + current_league_name + "_stats` WHERE `player_id`=" + away_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_steals, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_steals = total_steals + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_blocks = "SELECT blocks FROM `" + current_league_name + "_stats` WHERE `player_id`=" + away_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_blocks, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_blocks = total_blocks + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_tmade = "SELECT threesmade FROM `" + current_league_name + "_stats` WHERE `player_id`=" + away_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_tmade, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_threesmade = total_threesmade + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                    String sql_tatt = "SELECT threesattempted FROM `" + current_league_name + "_stats` WHERE `player_id`=" + away_id + " LIMIT " + i + ",1; ";

                    sqlCmd = new MySqlCommand(sql_tatt, sqlConn);
                    sqlRdHS1 = sqlCmd.ExecuteReader();

                    while (sqlRdHS1.Read())
                    {

                    }

                    total_threesatt = total_threesatt + Int32.Parse(sqlRdHS1[0].ToString());
                    sqlRdHS1.Close();

                }

                if (away_gamecount == 0)
                {
                    StarPlayerStatsAway = new PlayerStat(away_id, current_match_id, StarPlayerAway.FirstName, StarPlayerAway.LastName, 0, 0, 0, 0, 0, 0, 0);
                }
                else
                {
                    average_points = total_points / away_gamecount;
                    average_rebounds = total_rebounds / away_gamecount;
                    average_assists = total_assists / away_gamecount;
                    average_steals = total_steals / away_gamecount;
                    average_blocks = total_blocks / away_gamecount;
                    average_threesmade = total_threesmade / away_gamecount;
                    average_threesatt = total_threesatt / away_gamecount;

                    StarPlayerStatsAway = new PlayerStat(away_id, current_match_id, StarPlayerAway.FirstName, StarPlayerAway.LastName, average_points, average_rebounds, average_assists, average_steals, average_blocks, average_threesmade, average_threesatt);
                }

                sqlConn.Close();

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task DeleteMatch()
        {
            try
            {
                bool answer = await Shell.Current.DisplayAlert("Attention", "Are you sure you want to delete this match?", "Yes", "No");

                if (answer)
                {
                    sqlConn.Open();

                    String sql_delete = "DELETE FROM `" + current_league_name + "` WHERE `m_id`=" + current_match_id + "; ";

                    sqlCmd = new MySqlCommand(sql_delete, sqlConn);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    sqlRd2.Close();

                    sql_delete = "DELETE FROM `" + current_league_name + "_stats` WHERE `match_id`=" + current_match_id + "; ";

                    sqlCmd = new MySqlCommand(sql_delete, sqlConn);

                    sqlRd2 = sqlCmd.ExecuteReader();

                    while (sqlRd2.Read())
                    {

                    }

                    sqlRd2.Close();

                    sqlConn.Close();

                    Application.Current.MainPage.DisplayAlert("", "The match has been succesfully deleted!", "OK");

                    await Shell.Current.GoToAsync(nameof(LeaguesPage));
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
