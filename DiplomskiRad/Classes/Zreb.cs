using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Zreb
    {
        private Takmicenje takmicenje;
        public List<Kolo> listaKola;
        

        public Zreb(Takmicenje takmicenje)
        {
            this.takmicenje = takmicenje;
            listaKola = new List<Kolo>();
        }

        public void KreirajZreb()
        {
            double brojKola = Math.Log2(takmicenje.GetBrojUcesnika());
            KreirajKola(brojKola);
            GenerisiProtivnike(listaKola[0], takmicenje.GetUcesnici());
            
        }

        private void GenerisiProtivnike(Kolo prvoKolo, List<Ucesnik> ucesnici)
        {
            Random random = new Random();
            Ucesnik prviUcesnik;
            Ucesnik drugiUcesnik;
            int i = 0;
            while(ucesnici.Count > 0)
            {
                int randomNumber = random.Next(ucesnici.Count());
                prviUcesnik = ucesnici[randomNumber];
                ucesnici.RemoveAt(randomNumber);
                randomNumber = random.Next(ucesnici.Count());
                drugiUcesnik = ucesnici[randomNumber];
                ucesnici.RemoveAt(randomNumber);

                Mec mec = new Mec(i, prvoKolo);
                mec.unesiUcesnika(prviUcesnik);
                mec.unesiUcesnika(drugiUcesnik);

                prvoKolo.DodajMec(mec);
            }
        }

        private void KreirajKola(double brojKola)
        {
            for(int i=0; i<brojKola; i++)
            {
                listaKola.Add(new Kolo(i + 1, this));
            }
        }

    }
}
