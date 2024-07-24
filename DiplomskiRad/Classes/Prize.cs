using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Prize
    {
        public int Id { get; set; }
        public int placeNumber { get; set; }
        public double priceAmount { get; set; }
        public double prizePercentage { get; set; }

        public Tournament tournament { get; set; }

        public Prize(int number, double amount, double percentage, Tournament t)
        {
            this.placeNumber = number;
            this.priceAmount = amount;
            this.prizePercentage = Math.Round(percentage,2);
            this.tournament = t;
        }

    }
}
