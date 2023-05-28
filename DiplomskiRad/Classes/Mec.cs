using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Mec
    {
        
        private int mecID;
        private Ucesnik prviUcesnik;
        private Ucesnik drugiUcesnik;
        private Ucesnik pobednikMeca;
        private Kolo kolo;

        public Mec(int mecID, Kolo kolo)
        {
            this.mecID = mecID;
            this.kolo = kolo;
        }

        public void unesiPobednika(Ucesnik pobednik)
        {
            this.pobednikMeca = pobednik;
        }

        public void unesiUcesnika(Ucesnik ucesnik)
        {
            if(this.prviUcesnik == null)
            {
                prviUcesnik = ucesnik;
            }
            else
            {
                drugiUcesnik = ucesnik;
            }
        }


       public String GetPrviUcesnik()
        { 
            return prviUcesnik.GetNazivUcesnika();
        }
        public String GetDrugiUcesnik()
        {
            return drugiUcesnik.GetNazivUcesnika();
        }
        public Ucesnik GetPobednikMeca()
        {
            return pobednikMeca;
        }




    }
}
