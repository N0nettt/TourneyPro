using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Ekipa : Ucesnik
    {
        public List<Takmicar> takmicari;

        public Ekipa(string nazivUcesnika) : base(nazivUcesnika)
        {
            takmicari = new List<Takmicar>();
        }

        public List<Takmicar> GetTakmicari()
        {
            return takmicari;
        }

        public void SetTakmicari(List<Takmicar> listaTakmicara)
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
