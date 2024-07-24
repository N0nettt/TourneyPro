using DiplomskiRad.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Bracket
    {
        public int BracketID { get; set; }
        public Tournament tournament;
        public ObservableCollection<Round> listOfRounds;
        
        public Bracket(Tournament tournament)
        {
            this.tournament = tournament;
            listOfRounds = new ObservableCollection<Round>();
        }
        public void ConstructBracket()
        {
            double numOfRounds = Math.Log2(tournament.GetNumberOfParticipants());
            CreateRounds(numOfRounds);
            GenerateOppononets(listOfRounds, tournament.GetParticipants());
            int matchID = listOfRounds[0].GetMatches().Count() + 1;
            for(int i=1; i< listOfRounds.Count(); i++)
            {
                double matchesPerRound = MatchesPerRound(tournament.GetNumberOfParticipants(), i);
                for(int j=0; j<matchesPerRound; j++)
                {
                    Match match = new Match(matchID, listOfRounds[i]);
                    listOfRounds[i].AddMatch(match);
                    GlobalConfig.SqlConnection.CreateMatch(match);
                    matchID++;
                }
            }
        }
        // Returns number of mathces per round
        public double MatchesPerRound(int totalTeams, int currentRound)
        {
            var result = (totalTeams / Math.Pow(2, currentRound)) / 2;

            // Happens if you exceed the maximum possible rounds given number of teams
            if (result < 1.0F) throw new InvalidOperationException();

            return result;
        }

        //Selecting opponenets randomly for the matches of the 1st round
        private void GenerateOppononets(ObservableCollection<Round> listOfROunds, ObservableCollection<Participant> participants)
        {
            Random random = new Random();
            Participant firstParticipant;
            Participant secondParticipant;
            ObservableCollection<Participant> p = new ObservableCollection<Participant>(participants);
            int i = 1;
            while (p.Count > 0)
            {
                int randomNumber = random.Next(p.Count());
                firstParticipant = p[randomNumber];
                p.RemoveAt(randomNumber);
                randomNumber = random.Next(p.Count());
                secondParticipant = p[randomNumber];
                p.RemoveAt(randomNumber);
                Match match = new Match(i, listOfRounds[0]);
                GlobalConfig.SqlConnection.CreateMatch(match);
                match.addParticiapnt(firstParticipant);
                match.addParticiapnt(secondParticipant);
                listOfRounds[0].AddMatch(match);
                i++;
            }
        }

        private void CreateRounds(double numOfRounds)
        {
            for(int i=0; i<numOfRounds; i++)
            {
                Round r = new Round(i + 1, this);
                listOfRounds.Add(r);
                GlobalConfig.SqlConnection.CreateRound(r);
            }
        }

        public void ResetBracket()
        {
            foreach (Round r in listOfRounds)
            {
                r.matches.Clear();
                GlobalConfig.SqlConnection.DeleteMatches(r, this.tournament);
            }
            listOfRounds.Clear();
            GlobalConfig.SqlConnection.DeleteRounds(this);
        }

    }
}
