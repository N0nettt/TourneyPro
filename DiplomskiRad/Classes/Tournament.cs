using DiplomskiRad.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiplomskiRad.Classes
{
    public class Tournament
    {
        public int TournamentID { get; set; }
        public string tournamentName { get; set; }
        public DateTime date {  get ; set; }
        public int numberOfParticipants { get; set; }
        public ObservableCollection<Participant> participants { get; set; }
        public Participant? winner { get; set; }
        public Bracket bracket { get; set; }
        public bool managePayouts { get; set; }
        public ObservableCollection<Prize> prizes = new ObservableCollection<Prize>();
        public float entryFee { get; private set; }
        
        public User organizer { get; set; }
        // Constructor for creating the new tournament which is not in database
        public Tournament(string Name, DateTime date, int NumberOFParticipants, int Payouts, float Fee, User organizer)
        {
            this.tournamentName = Name;
            this.date = date;
            this.numberOfParticipants = NumberOFParticipants;
            if (Payouts == 1) { this.managePayouts = true; } else { this.managePayouts = false; }
            this.entryFee = Fee;
            participants = new ObservableCollection<Participant>();
            this.bracket = new Bracket(this);
            this.organizer = organizer;
        }
        // Constructor for creating the new tournament from database
        public Tournament(int TournamentID, string Name, DateTime date, int NumberOFParticipants, int Payouts, float Fee, User organizer)
        {
            this.TournamentID = TournamentID;
            this.tournamentName = Name;
            this.date = date;
            this.numberOfParticipants = NumberOFParticipants;
            if (Payouts == 1) { this.managePayouts = true; } else { this.managePayouts = false; }
            this.entryFee = Fee;
            participants = new ObservableCollection<Participant>();
            this.bracket = new Bracket(this);
            this.bracket.BracketID = GlobalConfig.SqlConnection.SelectBracketID(this);
            this.organizer = organizer;
        }
        #region Methods
        public void AnnounceWinner(Participant winner)
        {
            MessageBox.Show("The winner of the tournament is: " + winner.GetName(), "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            this.winner = winner;
            GlobalConfig.SqlConnection.SetWinner(winner, this);

        }
        public string GetWinner()
        {
            if (winner != null)
                return winner.GetName();
            else
                return "Tournament is not finished!";
        }

        public bool ConstructBracket()
        {
            if (participants.Count == numberOfParticipants)
            {
                this.bracket.ConstructBracket();
                return true;
            }
            return false;
        }

        public bool GetManagePayouts()
        {
            return managePayouts;
        }

        public string GetTournamentName()
        {
            return tournamentName;
        }
        public void SetTournamentName(string name)
        {
            this.tournamentName = name;
        }

        public DateTime GetDate()
        {
            return date;
        }
        public void SetDate(DateTime date)
        {
            this.date = date;
        }

        public int GetNumberOfParticipants()
        {
            return numberOfParticipants;
        }
        public void SetNumberOfParticipants(int num)
        {
            this.numberOfParticipants = num;
        }
       
        public ObservableCollection<Participant> GetParticipants()
        {
            return participants;
        }

        public bool AddParticipant(Participant p)
        {
            if(participants.Count < numberOfParticipants)
            {
                p = GlobalConfig.SqlConnection.CreateParticipant(p);
                GlobalConfig.SqlConnection.CreateTournamentParticipant(p, this);
                participants.Add(p);
                return true;
            }
            return false;
        } 

        public void SetParticipants(ObservableCollection<Participant> p)
        {
            this.participants = p;
        }

        public void RemoveParticipant(Participant p)
        {
            participants.Remove(p);
            GlobalConfig.SqlConnection.DeleteTournamentParticipant(p, this);
        }

        public int GetNumOfRegisteredParticipants()
        {
            return participants.Count;
        }

        public void ResetBracket()
        {
            bracket.ResetBracket();

        }
        // Putting participant on the selected index position
        public void SetSpecificParticipant(int index, Participant p)
        {
            participants[index] = p;
        }

        #endregion
    }
}
