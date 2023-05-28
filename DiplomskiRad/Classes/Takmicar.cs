using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Takmicar : Ucesnik
    {
        public string jmbg { get; set; }

        public Takmicar(string nazivUcesnika, string jmbg) : base(nazivUcesnika)
        {
            this.jmbg = jmbg;
        }

        public string GetJmbg()
        {
            return jmbg;
        }

        public void SetJmbg(string jmbg)
        {
            this.jmbg = jmbg;
        }




    }
}
