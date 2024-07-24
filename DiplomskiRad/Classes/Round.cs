using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Round
    {
        public ObservableCollection<Match> matches { get; set; }
        public int RoundID { get; set; }
        public int numOfRound { get; set; }
        public Bracket bracket;
        
        public Round(int numOfRound, Bracket bracket)
        {
            this.bracket = bracket;
            matches = new ObservableCollection<Match>();
            this.numOfRound = numOfRound;   
        }

        public void AddMatch(Match m)
        {
            matches.Add(m);
        }

        public ObservableCollection<Match> GetMatches()
        {
            return matches;
        }

        public int GetNumOfRound()
        {
            return numOfRound;
        }

        public bool CheckIfLastRound()
        {
            if(numOfRound == bracket.listOfRounds.Count)
            {
                return true;
            }
            return false;
        }
    }
}
