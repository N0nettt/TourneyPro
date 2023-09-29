using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Ekipa : Ucesnik
    {
        public ObservableCollection<Takmicar> takmicari;

        public Ekipa(string nazivUcesnika) : base(nazivUcesnika)
        {
            takmicari = new ObservableCollection<Takmicar>();
        }

        public ObservableCollection<Takmicar> GetTakmicari()
        {
            return takmicari;
        }

        public void SetTakmicari(ObservableCollection<Takmicar> listaTakmicara)
        {
            this.takmicari = listaTakmicara;
        }

        public void DodajTakmicara(Takmicar takmicar)
        {
            takmicari.Add(takmicar);
        }

        public void ObrisiTakmicara(Takmicar takmicar)
        {
            takmicari.Remove(takmicar);
            
        }

    }
}
