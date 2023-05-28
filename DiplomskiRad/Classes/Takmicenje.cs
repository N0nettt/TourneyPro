using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DiplomskiRad.Classes
{
    public class Takmicenje
    {
        private string nazivTakmicenja;
        private DateTime date;
        private int brojUcesnika;
        private string tipTakmicenja;
        private List<Ucesnik> ucesnici;
        private Ucesnik? pobednik;
        public Zreb zreb;
        

        public Takmicenje(string nazivTakmicenja, DateTime date, int brojUcesnika, string tipTakmicenja)
        {
            this.nazivTakmicenja = nazivTakmicenja;
            this.date = date;
            this.brojUcesnika = brojUcesnika;
            this.tipTakmicenja = tipTakmicenja;
            this.zreb = new Zreb(this);
            ucesnici = new List<Ucesnik>();        
        }

        public bool KreirajZreb()
        {
            if(ucesnici.Count() == brojUcesnika)
            {
                this.zreb.KreirajZreb();
                return true;
            }
            return false;
        }

        #region GettersAndSetters
        public void ProglasiPobednika(Ucesnik pobednik)
        {
            this.pobednik = pobednik;
        }
        public string GetPobednik()
        {
            if (pobednik != null)
                return pobednik.GetNazivUcesnika();
            else
                return "Pobednik nije proglasen";
        }

        public string GetNazivTakmicenja()
        {
            return nazivTakmicenja;
        }
        public void SetNazivTakmicenja(string naziv)
        {
            this.nazivTakmicenja = naziv;
        }

        public DateTime GetDate()
        {
            return date;
        }
        public void SetDate(DateTime date)
        {
            this.date = date;
        }

        public int GetBrojUcesnika()
        {
            return brojUcesnika;
        }
        public void SetBrojUcesnika(int brojUcesnika)
        {
            this.brojUcesnika = brojUcesnika;
        }
        public string GetTIpTakmicenja()
        {
            return tipTakmicenja;
        }
        public void SetTipTakmicenja(string tipTakmicenja)
        {
            this.tipTakmicenja = tipTakmicenja;
        }
        public List<Ucesnik> GetUcesnici()
        {
            return ucesnici;
        }

        public bool DodajUcesnika(Ucesnik ucesnik)
        {
            if(ucesnici.Count() < brojUcesnika)
            {
                ucesnici.Add(ucesnik);
                return true;
            }
            return false;
        } 

        public void SetUcesnici(List<Ucesnik> u)
        {
            this.ucesnici = u;
        }
        #endregion







    }
}
