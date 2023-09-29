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
    public class Mec
    {
        
        public int mecID { get; set; }
        public Ucesnik prviUcesnik { get; set; }
        public Ucesnik drugiUcesnik { get; set; }
        public Ucesnik pobednikMeca;
        public ObservableCollection<Ucesnik> ucesnici { get; set; }
        public int nextGame;
        private Kolo kolo;

        public Mec(int mecID, Kolo kolo)
        {
            this.mecID = mecID;
            this.kolo = kolo;
            ucesnici = new ObservableCollection<Ucesnik>();
            nextGame = (kolo.zreb.takmicenje.GetBrojUcesnika() / 2) + (int)Math.Ceiling((double)mecID / 2);
        }

        //Sends a winner of the match to the next match if its not the final
        public void unesiPobednika(Ucesnik pobednik)
        {
            //Checks if the match is the final, if yes announces winner of the tournament 
            if (kolo.CheckIfLastRound())
            {
                kolo.zreb.takmicenje.ProglasiPobednika(pobednik);
            }
            /*else
            {
                //Finds an appropriate match of the next round and move the winner to the match
                foreach (Mec m in kolo.zreb.listaKola[kolo.GetBrojKola()].GetMecevi().Where(m => m.mecID == nextGame))
                {
                    m.unesiUcesnika(pobednik);  
                }
               
            }*/
            this.pobednikMeca = pobednik;
        }

        // Check to which game should winner of this match procceed 
        /*public int SledecaUtakmica(int totalTeams, int matchID)
        {
            return (totalTeams / 2) + (int)Math.Ceiling((double)matchID/2);
        }*/

        public void unesiUcesnika(Ucesnik ucesnik)
        {
            if (prviUcesnik == null)
               {
                    prviUcesnik = ucesnik;
                    ucesnici.Add(ucesnik);
               }
            else
               {
                    drugiUcesnik = ucesnik;
                    ucesnici.Add(ucesnik);
               }
        }

       public void updateUcesnik()
       {
            ucesnici.Clear();
            foreach(Mec m in kolo.zreb.listaKola[kolo.brojKola-2].mecevi)
            {
                if(m.nextGame == this.mecID)
                {
                    if (m.pobednikMeca != null)
                        this.unesiUcesnika(m.pobednikMeca);
                }
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
