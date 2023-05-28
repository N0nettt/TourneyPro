using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Kolo
    {
        private List<Mec> mecevi;
        private int brojKola;
        private Zreb zreb;

        public Kolo(int brojKola, Zreb zreb)
        {
            this.zreb = zreb;
            mecevi = new List<Mec>();
            this.brojKola = brojKola;
        }

        public void DodajMec(Mec mec)
        {
            mecevi.Add(mec);
        }

        public List<Mec> GetMecevi()
        {
            return mecevi;
        }



    }
}
