using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Kolo
    {
        public ObservableCollection<Mec> mecevi { get; set; }
        public int brojKola { get; set; }
        public Zreb zreb;
        

        public Kolo(int brojKola, Zreb zreb)
        {
            this.zreb = zreb;
            mecevi = new ObservableCollection<Mec>();
            this.brojKola = brojKola;
            
        }

        public void DodajMec(Mec mec)
        {
            mecevi.Add(mec);
        }

        public ObservableCollection<Mec> GetMecevi()
        {
            return mecevi;
        }

        public int GetBrojKola()
        {
            return brojKola;
        }

        public bool CheckIfLastRound()
        {
            if(brojKola == zreb.listaKola.Count)
            {
                return true;
            }
            return false;
        }
    }
}
