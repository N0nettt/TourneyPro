using Dapper;
using DiplomskiRad.Classes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiplomskiRad.Database
{
    public class SqlConnector : IDataConnection
    {
        /// <summary>
        /// Saves the new bracket to the database
        /// </summary>
        /// <param name="bracket">The bracket's information</param>
        /// <returns>The bracket</returns>
        public int CreateBracket(Bracket bracket)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
            {
                var p = new DynamicParameters();
                p.Add("@TournamentID", bracket.tournament.TournamentID);
                p.Add("@BracketID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spCreateBracket", p, commandType: CommandType.StoredProcedure);

                bracket.BracketID = p.Get<int>("@BracketID");

                return bracket.BracketID;
            }
        }
        /// <summary>
        /// Saves new match to the database
        /// </summary>
        /// <param name="match">The match information</param>
        /// <returns>Returns the saved match</returns>
        public void CreateMatch(Match match)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    var p = new DynamicParameters();
                    p.Add("@MatchID", match.MatchID);
                    p.Add("@RoundID", match.Round.RoundID);
                    p.Add("@NextMatch", match.NextMatch);
                    p.Add("@TournamentID", match.Round.bracket.tournament.TournamentID);
                    connection.Execute("dbo.spCreateMatch", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Saves new participant to the database
        /// </summary>
        /// <param name="participant">The participant information</param>
        /// <returns>Return the participant information</returns>
        public Participant CreateParticipant(Participant participant)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
            {
                // Check if participant with the given email already exists
                var existingParticipant = connection.QuerySingleOrDefault<Participant>(
                    "SELECT ParticipantID, Name, Email FROM Participants WHERE Email = @Email",
                    new { Email = participant.Email }
                );

                if (existingParticipant != null)
                {
                    // Participant with this email already exists, return the existing participant
                    return existingParticipant;
                }

                var p = new DynamicParameters();
                p.Add("@Name", participant.Name);
                p.Add("@Email", participant.Email);
                p.Add("@ParticipantID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spCreateParticipant", p, commandType: CommandType.StoredProcedure);

                participant.ParticipantID = p.Get<int>("@ParticipantID");

                return participant;
            }
        }
        /// <summary>
        /// Saves the ne price to the database
        /// </summary>
        /// <param name="prize">The price information</param>
        /// <returns>Returning the price information</returns>
        public Prize CreatePrize(Prize prize)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
            {
                var p = new DynamicParameters();
                p.Add("@placeNumber", prize.placeNumber);
                p.Add("@prizeAmount", prize.priceAmount);
                p.Add("@prizePercentage", prize.prizePercentage);
                p.Add("@TournamentID", prize.tournament.TournamentID);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spCreatePrize", p, commandType: CommandType.StoredProcedure);

                prize.Id = p.Get<int>("@id");

                return prize;
            }
        }
        /// <summary>
        /// Saves the new role to the database
        /// </summary>
        /// <param name="role">The role information</param>
        /// <returns>The role information</returns>
        public Role CreateRole(Role role)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
            {
                var p = new DynamicParameters();
                p.Add("@Role", role.RoleName);
                p.Add("@RoleID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spCreateRole", p, commandType: CommandType.StoredProcedure);

                role.RoleID = p.Get<int>("@RoleID");

                return role;
            }
        }
        /// <summary>
        /// Saves the new round to the database
        /// </summary>
        /// <param name="round">The round information</param>
        /// <returns>The round information</returns>
        public Round CreateRound(Round round)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
            {
                var p = new DynamicParameters();
                p.Add("@BracketID", round.bracket.BracketID);
                p.Add("@RoundNumber", round.numOfRound);
                p.Add("@RoundID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spCreateRound", p, commandType: CommandType.StoredProcedure);

                round.RoundID = p.Get<int>("@RoundID");

                return round;
            }
        }
        /// <summary>
        /// Saves a new tournament to the database
        /// </summary>
        /// <param name="Tournament">The tournament information.</param>
        /// <returns>The tournament information, including unique identfier.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Tournament CreateTournament(Tournament tournament)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
            {
                int payouts = 0;
                if (tournament.GetManagePayouts() == true) { payouts = 1; } else { payouts = 0; }
                var p = new DynamicParameters();
                p.Add("@Name", tournament.GetTournamentName());
                p.Add("@Date", tournament.GetDate());
                p.Add("@NumberOfParticipant", tournament.GetNumberOfParticipants());
                p.Add("@Payouts", payouts);
                p.Add("@Fee", tournament.entryFee);
                p.Add("@TournamentID", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Organizer", tournament.organizer.Username);

                connection.Execute("dbo.spCreateTournament", p, commandType: CommandType.StoredProcedure);

                tournament.TournamentID = p.Get<int>("TournamentID");

                return tournament;
            }
        }
        /// <summary>
        /// Saves the new user to the database
        /// </summary>
        /// <param name="user">The user information</param>
        /// <returns>The user information</returns>
        /// <exception cref="Exception">Handling different SQL Exception and general exception</exception>
        public User CreateUser(User user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@Username", user.Username);
                    p.Add("@Password", user.Password);
                    p.Add("@Email", user.Email);
                    p.Add("@RoleID", user.Role.RoleID);

                    connection.Execute("dbo.spRegisterUser", p, commandType: CommandType.StoredProcedure);

                    return user;
                }
                catch (SqlException ex)
                {
                    // Handle specific SQL errors here
                    if (ex.Number == 2601)
                    {
                        // Primary key(email) constraint
                        throw new Exception("This username is already taken. Please choose a different one.");
                    }
                    else if (ex.Number == 2627)
                    {
                        // Unique field(Username) constraint
                        throw new Exception("This email is already taken. Please choose a different one.");
                    }
                    else
                    {
                        // General SQL error
                        throw new Exception("An error occurred while registering the user. Please try again later.");
                    }
                }
                catch (Exception ex)
                {
                    // General exception handling
                    throw new Exception("An unexpected error occurred. Please try again.");
                }
            }
        }
        /// <summary>
        /// Logging the user
        /// </summary>
        /// <param name="username">Username information</param>
        /// <param name="password">Password information</param>
        /// <returns>The user information</returns>
        /// <exception cref="Exception">Handling different SQL Exception and general exception</exception>
        public User LoginUser(string username, string password)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "SELECT * FROM Users WHERE Username = @Username and Password = @Password";
                    var parameters = new { Username = username, Password = password };
                    var results = connection.Query(command, parameters);

                    if (results.Any())
                    {
                        var user = results.First();
                        string email = user.Email;
                        int roleID = user.RoleID;
                        command = "SELECT * From Roles where RoleID = @RoleID";
                        var parameterss = new { RoleID = roleID };
                        var result = connection.Query<Role>(command, parameterss);
                        Role role = result.First();
                        User u = new User(username, password, email, role);
                        return u;  
                    }
                    else
                    {
                        MessageBox.Show("The user with the provided username does not exist. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");
                return null;
            }
        }
        public int SelectBracketID(Tournament tournament)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "SELECT BracketID from Brackets where TournamentID = @TournamentID";
                    var parameters = new { TournamentID = tournament.TournamentID };
                    int bracketID = connection.QuerySingle<int>(command, parameters);
                    return bracketID;
                }
            }
            catch (Exception)
            {

                throw new Exception("An unexpected error occurred. Please try again.");
            }
        }
        /// <summary>
        /// Select all the tournament logged User is participating in
        /// </summary>
        /// <param name="user">The information about the Usser</param>
        /// <returns>The list of the tournaments</returns>
        /// <exception cref="Exception">Handling different SQL Exception and general exception</exception>
        public ObservableCollection<Tournament> SelectParticipantTournaments(User user)
        {
            ObservableCollection<Tournament> tournaments = new ObservableCollection<Tournament>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    var parameters = new { Username = user.Username };
                    string command = "SELECT p.ParticipantID FROM Participants p LEFT JOIN Users u ON u.Email = p.Email WHERE u.Username = @Username";
                    var participantIdResult = connection.QuerySingleOrDefault<int>(command, parameters);

                    if (participantIdResult != default)
                    {
                        var Params = new { ParticipantID = participantIdResult };
                        command = "SELECT t.* FROM dbo.TournamentParticipants tp RIGHT JOIN dbo.Tournaments t ON t.TournamentID = tp.TournamentID WHERE tp.ParticipantID = @ParticipantID";
                        var results = connection.Query(command, Params);
                        foreach (var result in results)
                        {
                            // Map query result fields to Tournament constructor parameters
                            string name = result.Name;
                            DateTime date = result.Date; // Assuming 'date' is a DateTime field in your database
                            int numberOfParticipants = result.NumberOfParticipant; // Adjust field name as per your actual column name
                            int payouts = result.Payouts; // Adjust field name as per your actual column name
                            float fee = (float)result.Fee; // Adjust field name as per your actual column name
                            int tournamentID = result.TournamentID; // Assuming 'TournamentID' is an int field in your database
                            string organizer = result.Organizer;
                            command = "SELECT * from Users WHERE Username = @Organizer";
                            var param = new { Organizer = organizer, Winner = result.Winner };
                            var res = connection.Query(command, param);
                            var u = res.First();
                            string email = u.Email;
                            int roleID = u.RoleID;
                            string username = u.Username;
                            string password = u.Password;
                            command = "SELECT * From Roles where RoleID = @RoleID";
                            var parameterss = new { RoleID = roleID };
                            var r = connection.Query<Role>(command, parameterss);
                            Role role = r.First();
                            User org = new User(username, password, email, role);
                            var win = connection.Query<Participant>("SELECT p.ParticipantID, p.Name, p.Email FROM Participants p INNER JOIN Tournaments t ON t.Winner =p.ParticipantID WHERE t.Winner = @Winner", param);
                            // Create Tournament object and add to ObservableCollection
                            Tournament t = new Tournament(tournamentID, name, date, numberOfParticipants, payouts, fee, org);
                            if (win.Any())
                            {
                                Participant winner = win.First();
                                t.winner = winner;
                            }
                          
                            tournaments.Add(t);
                        }
                    }

                    return tournaments;
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");
                
            }
        }
        public void SetWinner(Participant p, Tournament t)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "UPDATE Tournaments SET Winner = @ParticipantID WHERE TournamentID = @TournamentID";
                    var parameters = new { ParticipantID = p.ParticipantID, TournamentID = t.TournamentID };
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception)
            {

                throw new Exception("An unexpected error occurred. Please try again.");
            }
               
        }
        public void CreateTournamentParticipant(Participant p, Tournament t)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "INSERT INTO TournamentParticipants(TournamentID, ParticipantID) VALUES (@TournamentID, @ParticipantID)";
                    var parameters = new { TournamentID = t.TournamentID, ParticipantID = p.ParticipantID };
                    connection.Execute(command, parameters);
                }
            }
            catch(Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");
            }
        }
        public void DeleteTournamentParticipant(Participant p, Tournament t)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "DELETE FROM TournamentParticipants WHERE TournamentID = @TournamentID and ParticipantID = @ParticipantID";
                    var parameters = new { TournamentID = t.TournamentID, ParticipantID = p.ParticipantID };
                    connection.Execute(command, parameters);
                }
            }
            catch(Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");
            }
        }
        //Delete all the matches for specific round
        public void DeleteMatches(Round r, Tournament t)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "DELETE FROM Matches WHERE RoundID = @RoundID and TournamentID = @TournamentID";
                    var parameters = new { RoundID = r.RoundID, TournamentID = t.TournamentID };
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");
            }
        }
        // Delete all the rounds for specific bracket
        public void DeleteRounds(Bracket b)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "DELETE FROM Rounds WHERE BracketID = @BracketID";
                    var parameters = new { BracketID = b.BracketID};
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");
            }
        }
        //Update the participants of the match
        public void UpdateParticipants(Match m)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {

                    string command = "UPDATE Matches SET FirstParticipantID = @FirstParticipantID, SecondParticipantID = @SecondParticipantID WHERE MatchID = @MatchID";
                    var parameters = new
                    {
                        FirstParticipantID = m.FirstParticipant?.ParticipantID ?? (object)DBNull.Value,
                        SecondParticipantID = m.SecondParticipant?.ParticipantID ?? (object)DBNull.Value,
                        MatchID = m.MatchID
                    };
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        //Updates the winner of the match
        public void UpdateTheWinner(Match m, Participant p)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "UPDATE Matches SET WinnerID = @WinnerID WHERE MatchID = @MatchID";
                    var parameters = new { WinnerID = p.ParticipantID, MatchID = m.MatchID };
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");
            }
        }
        //Delete all the prizes for specific tournament
        public void DeletePrizes(Tournament t)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "DELETE FROM Prizes WHERE TournamentID = @TournamentID";
                    var parameters = new { TournamentID = t.TournamentID};
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");
            }
        }
        //Select the tournament by Organizer
        public ObservableCollection<Tournament> SelectTournamentsByOrganizer(User user)
        {
            ObservableCollection<Tournament> tournaments = new ObservableCollection<Tournament>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "SELECT * FROM Tournaments WHERE Organizer = @Organizer";
                    var parameters = new { Organizer = user.Username };
                    var results = connection.Query(command, parameters);

                    foreach (var result in results)
                    {
                        // Map query result fields to Tournament constructor parameters
                        string name = result.Name;
                        DateTime date = result.Date; // Assuming 'date' is a DateTime field in your database
                        int numberOfParticipants = result.NumberOfParticipant; // Adjust field name as per your actual column name
                        int payouts = result.Payouts; // Adjust field name as per your actual column name
                        float fee = (float)result.Fee; // Adjust field name as per your actual column name
                        int tournamentID = result.TournamentID; // Assuming 'TournamentID' is an int field in your database
                        string organizer = result.Organizer;
                        var param = new { Winner = result.Winner };
                        var win = connection.Query<Participant>("SELECT p.ParticipantID, p.Name, p.Email FROM Participants p INNER JOIN Tournaments t ON t.Winner =p.ParticipantID WHERE t.Winner = @Winner", param);
                        // Create Tournament object and add to ObservableCollection
                        Tournament t = new Tournament(tournamentID, name, date, numberOfParticipants, payouts, fee, user);
                        if (win.Any())
                        {
                            Participant winner = win.First();
                            t.winner = winner;
                        }

                        tournaments.Add(t);
                    }

                    return tournaments;
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");

            }

        }
        //Return all the participants
        public ObservableCollection<Participant> SelectParticipants()
        {
            ObservableCollection<Participant> participants = new ObservableCollection<Participant>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "SELECT * FROM Participants";
                    var results = connection.Query<Participant>(command);
                    foreach(var result in results)
                    {
                        participants.Add(result);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");

            }
            return participants;
        }
        //Editing Participant in database
        public void UpdateParticipant(Participant p)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "UPDATE Participants SET Name = @Name, Email = @Email WHERE ParticipantID = @ParticipantID";
                    var parameters = new { Name = p.Name, Email = p.Email, ParticipantID = p.ParticipantID };
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");

            }
        }
        //Load all the information about selected tournament
        public Tournament LoadTournament(Tournament t)
        {
            Tournament tournament = t;
            try
            {  
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    //Clear data
                    tournament.participants.Clear();
                    tournament.prizes.Clear();
                    tournament.bracket?.listOfRounds.Clear();

                    string command = "SELECT BracketID FROM Brackets WHERE TournamentID = @TournamentID";
                    var parameters = new { TournamentID = tournament.TournamentID };
                    int BracketID = connection.QuerySingle<int>(command, parameters);
                    Bracket bracket = new Bracket(tournament);
                    bracket.BracketID = BracketID;
                    tournament.bracket = bracket;
                    command = "SELECT ParticipantID from TournamentParticipants WHERE TournamentID = @TournamentID";
                    List<int> participantIds = connection.Query<int>(command, parameters).ToList();
                    foreach(int participantID in participantIds)
                    {
                        command = "SELECT * FROM Participants WHERE ParticipantID = @ParticipantID";
                        var par = new { ParticipantID = participantID };
                        var participants = connection.Query<Participant>(command, par);
                        foreach(Participant p in participants)
                        {
                            tournament.participants.Add(p);
                        }
                    }
                    if (tournament.managePayouts == true)
                    {
                        command = "SELECT * FROM Prizes WHERE TournamentID = @TournamentID";
                        var prizes = connection.Query(command, parameters);
                        if (prizes.Any())
                        {
                            foreach(var result in prizes)
                            {
                                int PlaceNumber = result.placeNumber;
                                double PrizeAmount = result.prizeAmount;
                                double PrizePercentage = result.prizePercentage;
                                int Id = result.id;

                                Prize prize = new Prize(PlaceNumber, PrizeAmount, PrizePercentage, tournament);
                                prize.Id = Id;

                                tournament.prizes.Add(prize);
                            }
                        }
                    }
                    command = "SELECT * FROM Rounds WHERE BracketID = @BracketID";
                    var parametersTwo = new { BracketID = bracket.BracketID };
                    var results = connection.Query(command, parametersTwo);
                    
                    if (results.Any())
                    {
                        foreach(var result in results)
                        {
                            int RoundNumber = result.RoundNumber;
                            int RoundID = result.RoundID;

                            Round round = new Round(RoundNumber, bracket);
                            round.RoundID = RoundID;

                            command = "SELECT * FROM Matches WHERE RoundID = @RoundID and TournamentID = @TournamentID";
                            var parametersThree = new { RoundID = round.RoundID, TournamentID = tournament.TournamentID };
                            var matches = connection.Query(command, parametersThree);
                            if (matches.Any())
                            {
                                foreach(var m in matches)
                                {
                                    int MatchID = m.MatchID;
                                    int? FirstParticipantID = m.FirstParticipantID as int?;
                                    int? SecondParticipantID = m.SecondParticipantID as int?;
                                    int? WinnerID = m.WinnerID as int?;
                                    int NextMatch = m.NextMatch;

                                    Match match = new Match(MatchID, round);
                                    if(FirstParticipantID.HasValue)
                                    {
                                        match.FirstParticipant = tournament.participants.FirstOrDefault(p => p.ParticipantID == FirstParticipantID);
                                        match.participants.Add(match.FirstParticipant);
                                    }
                                    if(SecondParticipantID.HasValue)
                                    {
                                        match.SecondParticipant = tournament.participants.FirstOrDefault(p => p.ParticipantID == SecondParticipantID);
                                        match.participants.Add(match.SecondParticipant);
                                    }
                                    if(WinnerID.HasValue)
                                    {
                                        match.Winner = tournament.participants.FirstOrDefault(p => p.ParticipantID == WinnerID);
                                    }                          
                                    match.NextMatch = NextMatch;
                                    //Add match to the round's list of matches
                                    round.matches.Add(match);
                                }
                            }
                            bracket.listOfRounds.Add(round);
                        }
                    }
                    return tournament;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Removing the tournament from the database
        public void DeleteTournament(Tournament t)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "DELETE FROM Tournaments WHERE TournamentID = @TournamentID ";
                    var parameters = new { TournamentID = t.TournamentID };
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");

            }
        }
        //Return all the users from  database
        public ObservableCollection<User> SelectUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "SELECT * FROM Users";
                    var results = connection.Query(command);
                    foreach(var result in results)
                    {
                        string username = result.Username;
                        string password = result.Password;
                        string email = result.Email;
                        int roleID = result.RoleID;
                        command = "SELECT * FROM Roles WHERE RoleID = @RoleID";
                        var parameters = new { RoleID = roleID };
                        var role = connection.Query<Role>(command, parameters);
                        Role r = role.First();
                        User u = new User(username, password, email, r);
                        users.Add(u);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");

            }

            return users;

        }
        //Updating specific user in database
        public void UpdateUser(User user)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "UPDATE Users SET Username = @Username, Email = @Email, RoleID = @RoleID WHERE Username = @Username";
                    var parameters = new {Username = user.Username, Email = user.Email, RoleID = user.Role.RoleID};
                    connection.Execute(command, parameters);
                }               
            }
            catch (SqlException ex)
            {
                // Handle specific SQL errors here
                if (ex.Number == 2601)
                {
                    // Primary key(email) constraint
                    throw new Exception("This username is already taken. Please choose a different one.");
                }
                else if (ex.Number == 2627)
                {
                    // Unique field(Username) constraint
                    throw new Exception("This email is already taken. Please choose a different one.");
                }
                else
                {
                    // General SQL error
                    throw new Exception(ex.Message);
                }
            }
        }
        public void DeleteUser(User user)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "DELETE FROM Users WHERE Username = @Username";
                    var parameters = new { Username = user.Username };
                    connection.Execute(command, parameters);
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");

            }

        }
        // Retreiving roles from database
        public ObservableCollection<Role> SelectRoles()
        {
            ObservableCollection<Role> roles = new ObservableCollection<Role>();
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    string command = "SELECT * FROM Roles";
                    var results = connection.Query<Role>(command);
                    foreach (var result in results)
                    {
                        roles.Add(result);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred. Please try again.");

            }

            return roles;
        }

        public void UpdateTournament(Tournament t)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("TourneyProApp")))
                {
                    int payouts;
                    if (t.managePayouts ) { payouts = 1; } else { payouts = 0; }
                    string command = "UPDATE Tournaments SET Name = @Name, NumberOfParticipant = @NumOfPart, Payouts = @Payouts, Fee = @Fee WHERE TournamentID = @Id";
                    var parameters = new { Name = t.tournamentName, NumOfPart = t.numberOfParticipants, Payouts = payouts, Fee = t.entryFee, Id = t.TournamentID };

                    connection.Execute(command, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong, please try again.");

            }
        }

    }
}

