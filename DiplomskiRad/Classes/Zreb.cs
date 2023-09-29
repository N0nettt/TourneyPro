using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Zreb
    {
        public Takmicenje takmicenje;
        public ObservableCollection<Kolo> listaKola;
        
        public Zreb(Takmicenje takmicenje)
        {
            this.takmicenje = takmicenje;
            listaKola = new ObservableCollection<Kolo>();
        }
        public void KonstruisiZreb()
        {
            double brojKola = Math.Log2(takmicenje.GetBrojUcesnika());
            KreirajKola(brojKola);
            GenerisiProtivnike(listaKola, takmicenje.GetUcesnici());
            int mecID = listaKola[0].GetMecevi().Count() + 1;
            for(int i=1; i<listaKola.Count(); i++)
            {
                double mecevaPoRundi = MecevaPoRundi(takmicenje.GetBrojUcesnika(), i);
                for(int j=0; j<mecevaPoRundi; j++)
                {
                    Mec mec = new Mec(mecID, listaKola[i]);
                    listaKola[i].DodajMec(mec);
                    mecID++;
                }
            }
   
        }

        // Returns number of mathces per round
        public double MecevaPoRundi(int totalTeams, int currentRound)
        {
            var result = (totalTeams / Math.Pow(2, currentRound)) / 2;

            // Happens if you exceed the maximum possible rounds given number of teams
            if (result < 1.0F) throw new InvalidOperationException();

            return result;
        }

        //Selecting opponenets randomly for the matches of the 1st round
        private void GenerisiProtivnike(ObservableCollection<Kolo> listaKola, ObservableCollection<Ucesnik> ucesnici)
        {
            Random random = new Random();
            Ucesnik prviUcesnik;
            Ucesnik drugiUcesnik;
            ObservableCollection<Ucesnik> u = new ObservableCollection<Ucesnik>(ucesnici);
            int i = 1;
            while (u.Count > 0)
            {
                int randomNumber = random.Next(u.Count());
                prviUcesnik = u[randomNumber];
                u.RemoveAt(randomNumber);
                randomNumber = random.Next(u.Count());
                drugiUcesnik = u[randomNumber];
                u.RemoveAt(randomNumber);
                Mec mec = new Mec(i, listaKola[0]);
                mec.unesiUcesnika(prviUcesnik);
                mec.unesiUcesnika(drugiUcesnik);
                listaKola[0].DodajMec(mec);
                i++;
            }
        }

        private void KreirajKola(double brojKola)
        {
            for(int i=0; i<brojKola; i++)
            {
                listaKola.Add(new Kolo(i + 1, this));
            }
        }

        public void ResetBracket()
        {
            foreach (Kolo k in listaKola)
            {
                k.mecevi.Clear();
            }
                listaKola.Clear();
            
        }

    }
}
