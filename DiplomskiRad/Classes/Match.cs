using DiplomskiRad.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DiplomskiRad.Classes
{
    public class Match
    {
        public int MatchID { get; set; }
        public Participant? FirstParticipant { get; set; }
        public Participant? SecondParticipant { get; set; }
        public Participant? Winner;
        public ObservableCollection<Participant> participants { get; set; }
        public int NextMatch;
        public Round Round;
        //Checking if FirstParticipant is Winner
        public bool IsFirstParticipantWinner => Winner != null && Winner == FirstParticipant;
        //Checkign if SecondParticipant is Winner
        public bool IsSecondParticipantWinner => Winner != null && Winner == SecondParticipant;

        public Match(int matchID, Round round)
        {
            this.MatchID = matchID;
            this.Round = round;
            participants = new ObservableCollection<Participant>();
            NextMatch = (round.bracket.tournament.GetNumberOfParticipants() / 2) + (int)Math.Ceiling((double)matchID / 2);
        }

        //Sends a winner of the match to the next match if its not the final
        public void SetWinner(Participant winner)
        {
            //Checks if the match is the final, if yes announces winner of the tournament 
            if (Round.CheckIfLastRound())
            {
                Round.bracket.tournament.AnnounceWinner(winner);
            }
            /*else
            {
                //Finds an appropriate match of the next round and move the winner to the match
                foreach (Mec m in kolo.zreb.listaKola[kolo.GetBrojKola()].GetMecevi().Where(m => m.mecID == nextGame))
                {
                    m.unesiUcesnika(pobednik);  
                }
               
            }*/
            this.Winner = winner;
            GlobalConfig.SqlConnection.UpdateTheWinner(this, winner);
        }

        // Check to which game should winner of this match procceed 
        /*public int SledecaUtakmica(int totalTeams, int matchID)
        {
            return (totalTeams / 2) + (int)Math.Ceiling((double)matchID/2);
        }*/

        public void addParticiapnt(Participant p)
        {
            if (FirstParticipant == null)
            {
                FirstParticipant = p;
                participants.Add(p);
            }
            else
            {
                SecondParticipant = p;
                participants.Add(p); 
            }
            GlobalConfig.SqlConnection.UpdateParticipants(this);
        }

        public void updateUcesnik()
        {
            participants.Clear();
            this.FirstParticipant = null;
            this.SecondParticipant = null;
            foreach (Match m in Round.bracket.listOfRounds[Round.numOfRound - 2].matches)
            {
                if (m.NextMatch == this.MatchID)
                {
                    if (m.Winner != null)
                        this.addParticiapnt(m.Winner);
                }
            }
            
        }


    }
}
