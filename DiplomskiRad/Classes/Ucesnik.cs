using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Ucesnik
    {
        public string nazivUcesnika { get; set; }
        

        public string GetNazivUcesnika()
        {
            return this.nazivUcesnika;
        }

        public void SetNazivUcesnika(string naziv)
        {
            this.nazivUcesnika = naziv;
        }

        public Ucesnik(string nazivUcesnika)
        {
            this.nazivUcesnika = nazivUcesnika;
        }


        
    }
}
